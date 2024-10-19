# To-Do-List_MVC

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Screenshots](#screenshots)
- [Restore Database from Backup](#restore-database-from-backup)

## Overview
A simple ASP.NET Core MVC application for managing tasks with user authentication. Users can create, edit, and delete tasks and manage their profile.
 The project includes role-based authentication and a responsive user interface.

## Features
- **User Authentication**: Register, login, and logout functionality using cookie-based authentication.
- **User Management**: Edit user profiles and delete accounts with confirmation.
- **Task Management**: Create, edit, delete, and view tasks. Tasks are associated with specific users.
- **Profile View**: Users can view their profile details and update their information.
- **Responsive Design**: Built with Bootstrap for a mobile-friendly interface.
- **Confirmation for Deletion**: Includes a modal confirmation for account deletion to prevent accidental data loss.

## Screenshots

![Login Screen](/Screenshots/Login.png)
![Task Management](/Screenshots/Tasks.png)
![Profile](/Screenshots/Profile.png)


### Restore Database from Backup
1. Open SQL Server Management Studio.
2. Right-click on `Databases` > `Restore Database...`.
3. Select `Device` and browse to `Database/your_database_backup.bak`.
4. Click `OK` to restore the database.
5. Update the connection string in `appsettings.json` to point to your restored database.