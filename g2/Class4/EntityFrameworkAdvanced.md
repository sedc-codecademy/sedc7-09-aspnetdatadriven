# Entity Framework Advanced

# DataBase First Aproach
The database first approach enables you to generate database context and domain model from an existing database using Entity Data Model(EDM) wizard for integrated in Visual Studio.

# Creating an Entity Data Model
Entity Framework uses EDM for all the database-related operations. Entity Data Model is a model that describes entities and the relationships between them. Let's create a simple EDM for the PizzaApp database using Visual Studio. Follow the following steps to generate the Domain model from database:

1. Create Class Library project in Visual studio
![Create ClassLibrary](https://raw.githubusercontent.com/sedc-codecademy/sedc7-09-aspnetdatadriven/master/g2/Class4/img/ClassLibrary.PNG)
2. After the project is created right-click on the project and click Add --> New Ite. From the displayed screen select Data and select ADO.NET Entity Data Model and provide an appropriate name to the EDM and click the Add button.
![Add New Item](https://raw.githubusercontent.com/sedc-codecademy/sedc7-09-aspnetdatadriven/master/g2/Class4/img/NewItem.JPG)
3. From the next screen select the "EF Designer from datanase" and click Next >.
![Select Model Content](https://raw.githubusercontent.com/sedc-codecademy/sedc7-09-aspnetdatadriven/master/g2/Class4/img/WizzardStep1.JPG)
4. On "Choose Your Data Connection" screen click on "New Connection" button to add your SqlServer/database connection.
![New Connection](https://raw.githubusercontent.com/sedc-codecademy/sedc7-09-aspnetdatadriven/master/g2/Class4/img/WizzardStep2.JPG)
5. In the "New Connection" screen insert the SQL Server name (##Local machine name##\SQLExpress or just ##Local machine name## ex. PALMYRA01), specify the authentication type SQL Server Authentication or Windows Authentication, insert user and password if SQL Server Authentication selec the database that you want to use for domain model vgeneration. Before click on "OK" button validate that the connection is OK by clicking on "Test Connection" button, and if everything is OK then click on "OK" button.
6. ![Setup Database Connection](https://raw.githubusercontent.com/sedc-codecademy/sedc7-09-aspnetdatadriven/master/g2/Class4/img/WizzardStep3.JPG)
6. On "Choose Your Data Connection" screen if radio button "Yes, include the sensitive data in the connection string". Insert the connection setting name in "Save connection settings in App.Config as:" and click on "Next >".
![Choose Your Data Connection](https://raw.githubusercontent.com/sedc-codecademy/sedc7-09-aspnetdatadriven/master/g2/Class4/img/WizzardStep4.JPG)
7. On "Choose your version" screen select the "Entity Framework 6.x" radio button and click "Next >"
![Choose Entity Framework version](https://raw.githubusercontent.com/sedc-codecademy/sedc7-09-aspnetdatadriven/master/g2/Class4/img/WizzardStep5.JPG)
8. On "Choose Your Database Objects and Settings" select the tables from the tree view for wich you want to generate models. From the check boxes vellow the tree view select all of them, insert the Model Namespace, and click on "Finish".
![Database Objects and Settings](https://raw.githubusercontent.com/sedc-codecademy/sedc7-09-aspnetdatadriven/master/g2/Class4/img/WizzardStep6.JPG)
9. This will generate the cntext and the entity classes in the curent project.
![Data Model](https://raw.githubusercontent.com/sedc-codecademy/sedc7-09-aspnetdatadriven/master/g2/Class4/img/WizzardStep7.JPG)

After the wizard is finished you will have the needed context and the domain model and you will be able to use it in any projects. You can create your application architecture accirding the requirements and use the context to access the database. For example if you use n-tire arhcitecture you will need to include the repositories in the current project and create several other projects (Services, View Model, Mapper, Web Application ...). You will have to install entity framework on Web Application and in Web.Config use the Connection string that was added in App.Config in the current application in order to be able to use the context.
