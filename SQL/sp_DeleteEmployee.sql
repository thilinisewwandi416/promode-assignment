USE [Promode]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteEmployee]    Script Date: 7/25/2025 7:14:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_DeleteEmployee]
    @EmployeeID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM Employees
    WHERE EmployeeID = @EmployeeID;
    
    RETURN @@ROWCOUNT;
END;