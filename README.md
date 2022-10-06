# HangmanAPI
HangmanAPI is a simple simulation of the hangman game. It contains a CRUD interface for adding new words and the ability to create games and make guesses on them.

#Setup
Pre-requisites: Docker desktop using Linux Containers.
To run the application locally.

1. Open the solution file.
2. Run "docker-compose" to create the database and api containers.
3. The application should run and the swagger page should display.

#Running the tests.
In order to run the tests, you first must create the containers so the database is accessible. Alternatively, you can point the connection string to a different database.
The connection string will need to be changed to the following due to how the network bridge for docker works:
`"Server=localhost;Database=HangmanAPI;User=sa;Password=H@ngM@n!219;"`
