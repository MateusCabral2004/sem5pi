const fs = require('fs');
const os = require('os');
const path = require('path');

const HOSTNAME = 'myapp.local';
const IP = '10.9.10.8';

// Get the path to the hosts file depending on the OS
function getHostsPath() {
  return os.platform() === 'win32'
    ? path.join('C:', 'Windows', 'System32', 'drivers', 'etc', 'hosts')
    : '/etc/hosts'; // Linux and macOS
}

function ensureSudo() {
  if (process.getuid && process.getuid() !== 0) {
    console.error('This script must be run with sudo privileges!');
    process.exit(1);
  }
}

function addHostEntry(callback) {
  const hostsFile = getHostsPath();
  const entry = `${IP} ${HOSTNAME}`;

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

    // Prepare the new entry
    const newEntry = `${data.trim()}\n${entry}\n`;

    fs.writeFile(hostsFile, newEntry, 'utf8', (err) => {
      if (err) {
        console.error('Error writing to hosts file:', err);
        return;
      }
      console.log('Successfully added entry:', entry);
      callback();
    });
  });
}

function removeHostEntry(callback) {
  const hostsFile = getHostsPath();
  const entry = `${IP} ${HOSTNAME}`;

  fs.readFile(hostsFile, 'utf8', (err, data) => {
    if (err) {
      console.error('Error reading hosts file:', err);
      return;
    }

    // Remove the target entry
    const updatedData = data
      .split('\n')
      .filter((line) => line.trim() !== entry)
      .join('\n');

    fs.writeFile(hostsFile, updatedData, 'utf8', (err) => {
      if (err) {
        console.error('Error writing to hosts file:', err);
        return;
      }
      console.log('Successfully removed entry:', entry);
      callback();
    });
  });
}

const args = process.argv.slice(2);

ensureSudo();

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
