using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HRM.DataAccessEf.Entities;
using HRM.DataAccessEf.Interfaces;

namespace HRM.DataAccessEf.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRMContext _db;

        public EmployeeRepository(HRMContext context)
        {
            _db = context;
        }

        public Employee GetById(Guid id)
        {
            var res = _db.Employees.Find(id);
            
            if (res == null) throw new Exception("Employee not found");
            
            return res;
        }

        public void Add(Employee e)
        {
            _db.Employees.Add(e);
        }

        public void Update(Employee e)
        {
            _db.Entry(e).State = EntityState.Modified;
        }

        public (int totalCount, IEnumerable<Employee> items) Get(int? pageIndex = null, int? pageSize = null,
            Func<Employee, bool>? predicate = null)
        {
            var totalCount = _db.Employees.Where(predicate ?? (i => true)).Count();

            if (pageSize != null)
            {
                return (totalCount, _db.Employees
                    .Where(predicate ?? (i => true))
                    .Skip(((pageIndex ?? 1) - 1) * pageSize.Value).Take(pageSize.Value)
                    .OrderBy(i => i.CreatedTime));
            }

            return (totalCount, _db.Employees
                .Where(predicate ?? (i => true))
                .OrderBy(i => i.CreatedTime));
        }

        public IEnumerable<Employee> GetAll(Func<Employee, bool>? predicate = null)
        {
            return _db.Employees
                .Where(predicate ?? (i => true))
                .OrderBy(i => i.CreatedTime);
        }

        public void Remove(Employee e)
        {
            _db.Employees.Remove(e);
        }
    }
}