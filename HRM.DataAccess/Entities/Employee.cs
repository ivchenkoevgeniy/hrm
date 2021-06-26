using System;
using HRM.DataAccessEf.Enums;

namespace HRM.DataAccessEf.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }  
        public DateTime CreatedTime { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public bool IsFullTime { get; set; }
    }
}