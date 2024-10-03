@echo off

REM Remove the last migration
echo Removing the last migration...
rmdir /s /q "Migrations"

REM Add a new migration
echo Adding a new migration...
dotnet ef migrations add InitialCreate

REM Delete the database and create a new one
echo Deleting the database...
mysql -u root -pqyDWqxuLLhI6 -h vsgate-s1.dei.isep.ipp.pt -P 10712 -e "DROP DATABASE sempi5; CREATE DATABASE sempi5;"

REM Update the database
echo Updating the database...
dotnet ef database update

echo Migration update completed successfully!
pause
