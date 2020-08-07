<P><B>Notes about project for system with JWT, REST APIs, Sql Server database.</B></P>

Used as .NET framework, .NET Core 3.1 , dependency Injection for sql connection, using a DbContext, UsersDbContext for the UserInfo class and for the JWT Authentication.

<P>Installed all the various NuGet packages:</P>
<UL>
<LI>Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design -Version 3.1.0</LI>
<LI>Install-Package Microsoft.EntityFrameworkCore.Tools -Version 3.1.0</LI>
<LI>Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 3.1.0</LI>
<LI>Install-Package System.IdentityModel.Tokens.Jwt -Version 5.6.0</LI>
<LI>Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 3.1.0</LI>
</UL>

<P>Created a database, using sql Server 2019. details below.</P>
<P>Connection string in the appsettings.json.</P>
<P>
Run following script to reverse engineer the database to create database context and entity POCO classes from tables. The scaffold command will create POCO class only for the tables that have a primary key:</P>
<P>Scaffold-DbContext “Server=(localdb)\MSSQLLocalDB;Database=UsersDb;Integrated Security=True” Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
</P>
<P>SERVER: (localdb)\MSSQLLocalDB </P>
<P>For the REST API, used the API Controller with actions using the Entity Framework template, using the context class UsersDbContext.</P>
<P>Both the REST APIs for the JWT controller and the UserInfo tested using Postman. </P>
<P>The second project, for the UI part, added a UserInfo controller, using EntityFramework for CRUD operations for the users tasks, i.e. Create/Edit/Delete etc. </P>
<UL>
<LI>Visual Studio 2019</LI>
<LI>ASP.NET Core 3.1</LI>
<LI>SQL Server 2019, localdb</LI>
<LI>Postman</LI>
</UL>

<P>Examples of the API endpoints:</P>
<P>https://localhost:44396/api/userInfo (GET) </P>
<P>https://localhost:44396/api/token (POST) </P>
-------------------------------------------
<BR>
<P>Create Table UserInfo(</P>
<P>UserId Int Identity(1,1) Not null Primary Key,</P>
<P>UserName Varchar(50) Not null,</P>
<P>Email Varchar(50) Not null,</P>
<P>Password Varchar(20) Not null,</P>
<P>CreatedDate DateTime Default(GetDate()) Not Null)</P>
<P>GO</P>
<P>CREATE NONCLUSTERED INDEX [IX_UserInfo_UserId]</P>
<P>    ON [dbo].[UserInfo]([UserId] ASC);</P>
<P>GO</P>
<P>ALTER TABLE [dbo].[UserInfo]</P>
<P>    ADD CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED ([UserId] ASC);</P>
<P>GO</P>
<P>Insert Into UserInfo(UserName, Email, Password) </P>
<P>Values ('user1','u1@email.com', 'psw1')</P>
<P>Insert Into UserInfo(UserName, Email, Password) </P>
<P>Values ('user2','u2@email.com', 'psw2')</P>
<P>
-------------------------------------------
</P>
<P>scripts details:</P>
Scaffold-DbContext “Server=(localdb)\MSSQLLocalDB;Database=UsersDb;Integrated Security=True” Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
<P>SERVER: (localdb)\MSSQLLocalDB</P>
Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UsersDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
