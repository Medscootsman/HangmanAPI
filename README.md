# HangmanAPI
HangmanAPI is a simple simulation of the hangman game. It contains a CRUD interface for adding new words and the ability to create games and make guesses on them.

# Setup
Pre-requisites: Docker desktop using Linux Containers.
To run the application locally.

1. Open the solution file.
2. Set the docker-compose project in solution explorer as the main start-up project.
3. Run docker-compose to create the database and api containers.
4. The application should run and the swagger page should display.

# Running the tests.
In order to run the tests, you first must create the containers so the database is accessible. Alternatively, you can point the connection string to a different database.

The connection string for development (appsettings.json) will need to be changed to use localhost as the server name for the database, instead of db when running the docker-compose for debugging. See example below

`"Server=localhost;Database=HangmanAPI;User=sa;Password=examplepassword;"`
