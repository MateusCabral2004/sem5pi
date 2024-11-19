const fs = require('fs');
const os = require('os');
const path = require('path');
const sudo = require('sudo-prompt');

const HOSTNAME = 'myapp.local';
const IP = '10.9.10.8';

// Get the path to the hosts file depending on the OS
function getHostsPath() {
  if (os.platform() === 'win32') {
    return path.join('C:', 'Windows', 'System32', 'drivers', 'etc', 'hosts');
  } else {
    return '/etc/hosts'; // Linux and macOS
  }
}

function addHostEntry(callback) {
  const hostsFile = getHostsPath();
  const entry = `${IP} ${HOSTNAME}`;

  // Read the hosts file
  fs.readFile(hostsFile, 'utf8', (err, data) => {
    if (err) {
      console.error('Error reading hosts file:', err);
      return;
    }

    // Check if the entry already exists
    if (data.includes(entry)) {
      console.log(`Entry already exists: ${entry}`);
      callback();
      return;
    }

    // Prepare the new entry with a preceding newline
    const newEntry = `\n${entry}`;

    if (os.platform() === 'win32') {
      // Use PowerShell with Out-File to ensure proper newlines
      const command = `powershell -Command "(Get-Content '${hostsFile}') + '${entry}' | Out-File -FilePath '${hostsFile}' -Encoding UTF8"`;

      sudo.exec(command, { name: 'MyApp' }, (err, stdout, stderr) => {
        if (err) {
          console.error('Error adding entry on Windows:', err);
          return;
        }
        console.log('Successfully added entry on Windows:', entry);
        callback();
      });
    } else {
      // Use echo to append the new entry for Linux/macOS
      const command = `echo "${newEntry}" | sudo tee -a ${hostsFile} > /dev/null`;

      sudo.exec(command, { name: 'MyApp' }, (err, stdout, stderr) => {
        if (err) {
          console.error('Error adding entry on Linux/macOS:', err);
          return;
        }
        console.log('Successfully added entry on Linux/macOS:', entry);
        callback();
      });
    }
  });
}

function removeHostEntry(callback) {
  const hostsFile = getHostsPath();
  const entry = `${IP} ${HOSTNAME}`;

  // Read the hosts file
  fs.readFile(hostsFile, 'utf8', (err, data) => {
    if (err) {
      console.error('Error reading hosts file:', err);
      return;
    }

    // Normalize newlines to Windows format (\r\n)
    const normalizedData = data.replace(/\r?\n/g, '\r\n');

    // Check if the entry exists
    if (!normalizedData.includes(entry)) {
      console.log('No matching entry found to remove.');
      return callback();
    }

    // Remove the target entry
    const newData = normalizedData
      .split('\r\n')
      .filter(line => line.trim() !== entry)
      .join('\r\n');

    if (os.platform() === 'win32') {
      // Write content to a temporary file
      const tempFile = `${os.tmpdir()}\\hosts_temp`;

      fs.writeFile(tempFile, newData, 'utf8', (err) => {
        if (err) {
          console.error('Error creating temporary file:', err);
          return;
        }

        // Use PowerShell to overwrite the hosts file with the temporary file
        const command = `powershell -Command "Copy-Item -Path '${tempFile}' -Destination '${hostsFile}' -Force"`;

        sudo.exec(command, { name: 'MyApp' }, (err, stdout, stderr) => {
          // Clean up the temporary file
          fs.unlink(tempFile, () => {});

          if (err) {
            console.error('Error removing entry on Windows:', err, stderr);
            return;
          }

          console.log('Successfully removed entry on Windows:', entry);
          callback();
        });
      });
    } else {
      // Use echo and tee for Linux/macOS
      const command = `echo "${newData}" | sudo tee ${hostsFile} > /dev/null`;

      sudo.exec(command, { name: 'MyApp' }, (err, stdout, stderr) => {
        if (err) {
          console.error('Error removing entry on Linux/macOS:', err);
          return;
        }
        console.log('Successfully removed entry on Linux/macOS:', entry);
        callback();
      });
    }
  });
}

const args = process.argv.slice(2);

if (args.includes('--clean')) {
  console.log('Running cleanup...');
  removeHostEntry(() => {
    console.log('Host entry removed successfully.');
    process.exit();
  });
} else {
  console.log('Adding host entry...');
  addHostEntry(() => {
    console.log('Host entry added successfully.');
    process.exit();
  });
}
