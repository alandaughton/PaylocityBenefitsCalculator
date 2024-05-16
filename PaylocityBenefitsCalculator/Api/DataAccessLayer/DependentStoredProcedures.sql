-- These are the stored procedures that handle the requirements and conform to the database design.
-- NOTE: The requirement that 'An employee may only have 1 spouse or domestic partner (not both)' is enforced here.
--       The requirement that 'An employee may have an unlimited number of children' is also permitted here.
--       The GUI should also enforce these constraints, but it is important that the database cannot get corrupted.
CREATE PROCEDURE [dbo].[CreateDependent]
	@EmployeeId int,
	@Firstname int varchar(50),
	@Lastname int varchar(50),
	@DateOfBirth date,
	@Relationship tinyint
AS
BEGIN
	IF @Relationship = 0
		THROW 51000, 'Relationship cannot be zero', 1;

	IF (@Relationship = 1 OR @Relationship = 2)
		AND EXISTS(SELECT DependentId FROM [dbo].[Dependent] WHERE EmployeeId = @EmployeeId AND (Relationship = 1 OR Relationship = 2) )
			THROW 51000, 'Only a single Spouse or Domestic Partner allowed', 1;

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

	INSERT INTO [dbo].[Dependent]
	(
		PersonId,
		EmployeeId,
		Relationship
	)
	VALUES
	(
		@PersonId,
		@EmployeeId,
		@Relationship
	);

	SELECT SCOPE_IDENTITY();
END
GO

CREATE PROCEDURE [dbo].[ReadEmployeeDependents]
	@EmployeeId int
AS
BEGIN
	SELECT
		per.Firstname,
		per.Lastname,
		per.DateOfBirth,
		dep.Relationship
	FROM [dbo].[Dependent] dep
		JOIN [dbo].[Person] per
		ON per.PersonId = dep.PersonId
	WHERE dep.EmployeeId = @EmployeeId;
END
GO

CREATE PROCEDURE [dbo].[ReadAllDependents]
AS
BEGIN
	SELECT
		per.Firstname,
		per.Lastname,
		per.DateOfBirth,
		dep.EmployeeId,
		dep.Relationship
	FROM [dbo].[Dependent] dep
		JOIN [dbo].[Person] per
		ON per.PersonId = dep.PersonId;
END
GO

CREATE PROCEDURE [dbo].[ReadDependent]
	@DependentId int
AS
BEGIN
	SELECT
		per.Firstname,
		per.Lastname,
		per.DateOfBirth,
		dep.EmployeeId,
		dep.Relationship
	FROM [dbo].[Dependent] dep
		JOIN [dbo].[Person] per
		ON per.PersonId = dep.PersonId;
	WHERE dep.DependentId = @DependentId;
END
GO

-- I'm not going to bother with update and delete since they are not part of the requirements
