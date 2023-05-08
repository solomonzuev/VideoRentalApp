# VideoRentalApp

# Movie Rental System

This is a movie rental system that allows users to browse, search, and rent movies. The system includes authentication and authorization for users, as well as different levels of access for operators and administrators.

## Features

1. Authentication and authorization
2. View list of movies
3. Search movies by genre, actors, and format
4. Rent movies
	1. Choose the format of the movie
	2. Select the rental period
	3. Enter payment information (conditional payment)
	4. Receive information on where to pick up the movie after payment
		1. Pickup location address
5. View rented movies
6. View list of all movies (operator)
	1. Ability to edit movie information
	2. Ability to add new movies
7. View information about rented movies (operator)
	1. Ability to confirm movie return
	2. Ability to rent out a movie
	3. Ability to add user to blacklist (user will no longer be able to rent movies)
8. View information about actors
	1. Ability to edit actor information
	2. Ability to add new actors
9. Edit user information (operator)
10. Register new employee (administrator)
11. Design using MaterialDesignThemes library
12. Use EntityFrameworkCore to work with the database

## Technology Stack

The movie rental system is built using the following technologies:

- WPF
- C#
- EntityFrameworkCore
- MaterialDesignThemes

## Installation

To install and run the movie rental system, follow these steps:

1. Clone the repository.
2. Install the required packages by running `dotnet restore`.
3. Update the database by running `dotnet ef database update`.
4. Run the application by running `dotnet run`.

## Usage

After installing and running the movie rental system, users can browse, search, and rent movies. Operators can manage movies and rented movies, while administrators can register new employees. The application uses MaterialDesignThemes for a modern and responsive design. EntityFrameworkCore is used to work with the database.

## Contributors

This movie rental system was created by Solomon Zuev for coursework. Please feel free to contribute to this project by submitting a pull request.
