-- This is how I would design the tables.  There is no backing database for this project,
-- but I did want to provide a design.
CREATE TABLE [dbo].[Person]
(
	PersonId int PRIMARY KEY IDENTITY (1, 1) NOT NULL,
	Firstname varchar(50) NOT NULL,
	Lastname varchar(50) NOT NULL,
	DateOfBirth date NOT NULL,

	CONSTRAINT PK_Person_PersonId PRIMARY KEY CLUSTERED (PersonId)
)
GO

CREATE TABLE [dbo].[Employee]
(
	EmployeeId int PRIMARY KEY IDENTITY (1, 1) NOT NULL,
	PersonId int NOT NULL,
	Salary decimal(11,2) NOT NULL,

	CONSTRAINT PK_Employee_EmployeeId PRIMARY KEY CLUSTERED (EmployeeId),
	CONSTRAINT FK_Employee_PersonId FOREIGN KEY (PersonId) REFERENCES [dbo].[Person] (PersonId)
)
GO

CREATE TABLE [dbo].[Dependent]
(
	DependentId int PRIMARY KEY IDENTITY (1, 1) NOT NULL,
	PersonId int NOT NULL,
	EmployeeId int NOT NULL,
	Relationship tinyint NOT NULL,

	CONSTRAINT PK_Dependent_DependentId PRIMARY KEY CLUSTERED (DependentId),
	CONSTRAINT FK_Dependent_PersonId FOREIGN KEY (PersonId) REFERENCES [dbo].[Person] (PersonId),
	CONSTRAINT FK_Dependent_EmployeeId FOREIGN KEY (EmployeeId) REFERENCES [dbo].[Employee] (EmployeeId)
)
GO
