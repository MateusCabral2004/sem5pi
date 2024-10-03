#!/bin/bash

# Remove the last migration
echo "Removing the last migration..."
rm -rf ./Migrations

# Add a new migration
echo "Adding a new migration..."
dotnet ef migrations add InitialCreate

# Delete the table
echo "Deleting the table..."
mysql -u root -pqyDWqxuLLhI6 -h vsgate-s1.dei.isep.ipp.pt -P 10712 -e "DROP DATABASE Sempi5; CREATE DATABASE Sempi5;"


# Update the database
echo "Updating the database..."
dotnet ef database update

echo "Migration update completed successfully!"
