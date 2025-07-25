﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
            public int EmployeeID { get; set; } 
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Department { get; set; }
            public DateTime CreatedDate { get; set; }

    }
}
