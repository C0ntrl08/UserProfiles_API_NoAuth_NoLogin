# User_Profiles Backend without Authentication and Authorization

This is a backend .NET project, similar in structure to applications often built from scratch.
As it is only intended for training purposes, neither Authentication nor Authorisation is included.
The database is a MySQL table. The testing of the API endpoints was done with the integrated Swagger. As the projects starts it opens the webbrowser with the Swagger UI. 

## Description
The project idea was a Backend interface for saving User Profiles. Each Profile has a User ID, Username, Email, First Name, Last Name, Date of Birth, the date the profile was created and the date the profile was last modified.

Some of the project files also contain some hardcoded values. So-called Helpers and Models classes have been used. For testing with Swagger, so-called DataTransferObjects were also used.

The allocation of Controllers was also important to access the endpoints (CRUD operations).

## Features
• List all the saved users
• List all the users for the given UserId
• Create new user
• Update the already saved user

## Technologies Used
• ASP.NET

• MySQL

• Entity Framework

• LINQ

## Installation
1. Clone the repository
2. Install MySQL Database and create a database called "users"
3. Create a table called "user_profiles" with the following fields
```
create table user_profiles (
    UserId int auto_increment primary key,
    UserName varchar(30) NOT NULL unique,
    Email varchar(100) NOT NULL unique,
    FirstName varchar(150) NOT NULL,
    LastName varchar(150) NOT NULL,
    DateOfBirth Date NOT NULL,
    CreatedAt datetime,
    UpdatedAt datetime
);
```
4. Run and Build the project

## Usage
After the application has been started the standard browser with the Swagger UI will open. From there you can test all the APIs.
