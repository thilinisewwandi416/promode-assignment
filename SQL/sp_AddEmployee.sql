USE [Promode]
GO
/****** Object:  StoredProcedure [dbo].[sp_AddEmployee]    Script Date: 7/25/2025 7:14:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AddEmployee]
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @PhoneNumber NVARCHAR(15),
    @Department NVARCHAR(50),
    @EmployeeID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Employees (FirstName, LastName, Email, PhoneNumber, Department, CreatedDate)
    VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Department, GETDATE());
    
    SET @EmployeeID = SCOPE_IDENTITY();
    
    SELECT EmployeeID, FirstName, LastName, Email, PhoneNumber, Department, CreatedDate
    FROM Employees
    WHERE EmployeeID = @EmployeeID;
END;
