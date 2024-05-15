-- These are the stored procedures that handle the requirements and conform to the database design.
CREATE PROCEDURE [dbo].[CreateEmployee]
	@Firstname int varchar(50),
	@Lastname int varchar(50),
	@DateOfBirth date,
	@Salary decimal(11,2)
AS
BEGIN
	INSERT INTO [dbo].[Person]
	(
		Firstname,
		Lastname,
		DateOfBirth
	)
	VALUES
	(
		@Firstname,
		@Lastname,
		@DateOfBirth
	);

	DECLARE @PersonId int;
	SELECT @PersonId = SCOPE_IDENTITY();

	INSERT INTO [dbo].[Employee]
	(
		PersonId,
		Salary
	)
	VALUES
	(
		@PersonId,
		@Salary
	);

	SELECT SCOPE_IDENTITY();
END
GO

CREATE PROCEDURE [dbo].[ReadAllEmployees]
AS
BEGIN
	SELECT
		emp.EmployeeId,
		per.Firstname,
		per.Lastname,
		per.DateOfBirth,
		emp.Salary
	FROM [dbo].[Employee] emp
		JOIN [dbo].[Person] per
		ON emp.PersonId = per.PersonId;
END
GO

CREATE PROCEDURE [dbo].[ReadEmployee]
	@EmployeeId int
AS
BEGIN
	SELECT
		per.Firstname,
		per.Lastname,
		per.DateOfBirth,
		emp.Salary
	FROM [dbo].[Employee] emp
		JOIN [dbo].[Person] per
		ON emp.PersonId = per.PersonId
	WHERE emp.EmployeeId = @EmployeeId;
END
GO

-- I'm not going to bother with update and delete since they are not part of the requirements
