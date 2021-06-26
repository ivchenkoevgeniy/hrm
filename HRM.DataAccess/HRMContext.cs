using System;
using System.Data.Entity;
using HRM.DataAccessEf.Entities;
using HRM.DataAccessEf.Enums;

namespace HRM.DataAccessEf
{
    public class HRMContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        static HRMContext()
        {
            Database.SetInitializer(new HMRDbInitializer());
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasKey(p => p.Id);
            
            // TODO: research howto use nullable property. Like ef.core HasFilter. Or use a plain sql in migrations ??   
            // modelBuilder.Entity<Employee>()
            //     .HasIndex(d => new { d.PersonnelNumber, d.IsFullTime }).IsUnique();
        }
    }

    public class HMRDbInitializer : DropCreateDatabaseAlways<HRMContext>
    {
        protected override void Seed(HRMContext db)
        {
            db.Employees.Add(new Employee
            {
                Id = Guid.NewGuid(),
                Name = "Steve",
                Number = 1,
                Gender = Gender.Male,
                Birthday = DateTime.Parse("1985-01-10").Date,
                CreatedTime = DateTime.Parse("2021-06-26"),
                IsFullTime = true
            });

            db.Employees.Add(new Employee
            {
                Id = Guid.NewGuid(),
                Name = "Anna",
                Gender = Gender.Female,
                Birthday = DateTime.Parse("1996-05-08").Date,
                CreatedTime = DateTime.Parse("2021-06-26"),
                IsFullTime = false
            });

            base.Seed(db);
        }
    }
}