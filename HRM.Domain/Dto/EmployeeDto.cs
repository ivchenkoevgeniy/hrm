using System;
using HRM.DataAccessEf.Enums;

namespace HRM.Domain.DTO
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }  
        public bool IsFullTime { get; set; }
    }
}