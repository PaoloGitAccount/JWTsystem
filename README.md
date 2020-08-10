# JWTsystem
Notes about project for system with JWT, REST APIs, Sql Server database.

Used as .NET framework, .NET Core 3.1 , dependency Injection for sql connection, using a DbContext, UsersDbContext for the UserInfo class and for the JWT Authentication.

    Installed all the various NuGet packages:
    Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design -Version 3.1.0
    Install-Package Microsoft.EntityFrameworkCore.Tools -Version 3.1.0
    Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 3.1.0
    Install-Package System.IdentityModel.Tokens.Jwt -Version 5.6.0
    Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 3.1.0

Created a database, using sql Server 2019. details below.
connection string in the appsettings.json.
<p>Run following script to reverse engineer the database to create database context and entity POCO classes from tables. The scaffold command will create POCO class only for the tables that have a primary key:</p>
<p>Scaffold-DbContext “Server=(localdb)\MSSQLLocalDB;Database=UsersDb;Integrated Security=True” Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models </p>
<p>SERVER: (localdb)\MSSQLLocalDB</p>
<p>For the REST API, used the API Controller with actions using the Entity Framework template, using the context class UsersDbContext.</p>
<p>Both the REST APIs for the JWT controller and the UserInfo tested using Postman.</p>
<p>The second project, for the UI part, added a UserInfo controller, using EntityFramework for CRUD operations for the users tasks, i.e. Create/Edit/Delete etc.</p>
<ul>
    <li>Visual Studio 2019</li>
    <li>ASP.NET Core 3.1</li>
    <li>SQL Server 2019, localdb</li>
    <li>Postman</li>
    </ul>

Examples of the API endpoints:
<p>https://localhost:44396/api/userInfo (GET)</p>
<p>https://localhost:44396/api/token (POST) </p>

-------------------------------------------
<p>Create Table UserInfo(</p>
<p>UserId Int Identity(1,1) Not null Primary Key,</p>
<p>UserName Varchar(50) Not null,</p>
<p>Email Varchar(50) Not null,</p>
<p>Password Varchar(20) Not null,</p>
<p>CreatedDate DateTime Default(GetDate()) Not Null)</p>
<p>GO</p>
<p>CREATE NONCLUSTERED INDEX [IX_UserInfo_UserId]</p>
<p>    ON [dbo].[UserInfo]([UserId] ASC);</p>
<p>GO</p>
<p>ALTER TABLE [dbo].[UserInfo]</p>
<p>    ADD CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED ([UserId] ASC);</p>
<p>GO</p>
<p>Insert Into UserInfo(UserName, Email, Password) </p>
<p>Values ('user1','u1@email.com', 'psw1')</p>
<p>Insert Into UserInfo(UserName, Email, Password) </p>
<p>Values ('user2','u12@email.com', 'psw2')</p>
-------------------------------------------
<p>scripts details:</p>
<p>Scaffold-DbContext “Server=(localdb)\MSSQLLocalDB;Database=UsersDb;Integrated Security=True” Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models</p>
<p>SERVER: (localdb)\MSSQLLocalDB</p>
Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UsersDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False

 
