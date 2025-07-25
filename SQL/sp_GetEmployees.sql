USE [Promode]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetEmployees]    Script Date: 7/25/2025 7:54:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetEmployees]
    @PageNumber INT,
    @PageSize INT,
    @TotalCount INT OUTPUT,
    @SearchName NVARCHAR(100) = NULL, -- Optional search by name
    @SearchDepartment NVARCHAR(50) = NULL -- Optional search by department
AS
BEGIN
    SET NOCOUNT ON;

    -- Calculate total count with search filters
    SELECT @TotalCount = COUNT(*)
    FROM Employees
    WHERE (@SearchName IS NULL OR FirstName LIKE '%' + @SearchName + '%' OR LastName LIKE '%' + @SearchName + '%')
      AND (@SearchDepartment IS NULL OR Department LIKE '%' + @SearchDepartment + '%');

    -- Retrieve paginated results
    SELECT EmployeeID, FirstName, LastName, Email, PhoneNumber, Department, CreatedDate
    FROM Employees
    WHERE (@SearchName IS NULL OR FirstName LIKE '%' + @SearchName + '%' OR LastName LIKE '%' + @SearchName + '%')
      AND (@SearchDepartment IS NULL OR Department LIKE '%' + @SearchDepartment + '%')
    ORDER BY EmployeeID
    OFFSET (@PageNumber - 1) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY;
END;
