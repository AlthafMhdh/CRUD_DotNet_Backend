﻿namespace Backend.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public double Salary { get; set; }
        public string Department { get; set; }

    }
}
