# Mandatory 2

## How to install the database
First you need to clone the following git repo:
[Link](https://github.com/oliverlowbob/5_sem_databases_mandatory)

After pulling the repo, you need to forward engineer the database of the model 'school_protocol.mwb'
Then you run the following scripts populate_database.sql script from the root folder.
After this, you run each of the scripts from the following folders:
- functions
- stored procedures 
- triggers

After setting up the database, you need to cd into the Skoleprotokol folder, and open the Skoleprotokol.sln with Visual Studio 2019.
Make sure you have all the right modifications for VS2019 for working with .NET Core 3.1 and Entity Framework 5.
Also make sure that all the Nuget packages for the project have been installed properly.

Then you set the Skoleprotokol as startup project, and run the program.
You can see the endpoints in each controller, and test it using Postman.
We have added a Postman collection, which you can use to test some of the endpoints.
