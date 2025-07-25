USE [Promode]
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateEmployee]    Script Date: 7/25/2025 7:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateEmployee]
    @EmployeeID INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @PhoneNumber NVARCHAR(15),
    @Department NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Employees
    SET FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email,
        PhoneNumber = @PhoneNumber,
        Department = @Department,
        CreatedDate = GETDATE()
    WHERE EmployeeID = @EmployeeID;
    
    SELECT EmployeeID, FirstName, LastName, Email, PhoneNumber, Department, CreatedDate
    FROM Employees
    WHERE EmployeeID = @EmployeeID;
END;