using System;
using HRM.DataAccessEf.Entities;

namespace HRM.DataAccessEf.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Employee> Employees { get;}
        void Save();
    }
}