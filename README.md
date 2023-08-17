# VideoRentalApp

## Description

The VideoRentalApp is a comprehensive solution for a video rental store. It allows customers to search and browse through a collection of films, view details such as genre, release date, and availability, and rent films for a specified period. Customers can also return films they've rented. Operators can issue and receive films back, as well as manage information about films. Administrators have access to an admin panel where they can manage employee accounts and store details. The system includes authentication and authorization for different types of users.

## Features

Common Features:
1. Authentication and authorization

Customer Features:
1. View list of movies
2. Search Movies by genre, actors, and format
3. Rent movies
   1. Choose pickup location address
   2. Choose the format of the movie
   3. Select the rental period
4. View rented movies

Operator Features:
1. View list of movies
2. View rented movies
   1. Distributing and collecting films
3. Manage celebrities' information
4. Limited manage user accounts

Administrator Features:
1. Manage employee accounts
2. Manage rental store details

## Technology Stack

The VideoRentalApp is built using the following technologies:

- C#
- NET Core 6
- WPF
- EntityFrameworkCore
- MaterialDesignThemes
- Microsoft SQL Server

## Installing

1. Clone or download the project repository.
2. Open the project in Microsoft Visual Studio.
3. Ensure that Entity Framework is installed via NuGet Package Manager.
4. Build the project to resolve any dependencies.

## Executing Program

1. Build and run the application from within Microsoft Visual Studio.
2. The application will launch, showing a user-friendly interface for customers to explore films and make selections.
3. For the administrator, you have a prepared initial authentication email and password: adm1@example.com admin.
4. IMPORTANT: The application currently only features a Russian interface!

## License

This project is licensed under the MIT License - see the LICENSE.md file for details.
