using System;
using HRM.DataAccessEf.Entities;
using HRM.DataAccessEf.Interfaces;

namespace HRM.DataAccessEf.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly HRMContext _db;
        private readonly IEmployeeRepository _employeeRepository;

        public EFUnitOfWork()
        {
            _db = new HRMContext();
            _employeeRepository = new EmployeeRepository(_db);
        }
        
        public IRepository<Employee> Employees => _employeeRepository;

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            
            if (disposing)
            {
                _db.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}