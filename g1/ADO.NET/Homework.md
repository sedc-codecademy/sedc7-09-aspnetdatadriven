# Homework - SQL Client Repository 📝

In the source code solution there is a web application called **Todo04**, basically it's the Todo app that we were working as a workshop on the previous course. You need to use this app to do your homework.  
In brief, you need to change the already implemented Entity Framework repository, with a new one that will be using SqlClient.

## Technical Notes
* The repository class should implement IGenericRepository
* SqlClient should be used for communication with the database
* The new repository implementation should be registered in Dependency Injection configuration instead of the old one