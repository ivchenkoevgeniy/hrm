using System;
using System.Collections.Generic;
using HRM.Domain.DTO;

namespace HRM.Domain
{
    public interface IEmployeeService
    {
        void Create(EmployeeDto e);
        void Update(EmployeeDto e);
        void Remove(Guid id);
        EmployeeDto GetById(Guid id);
        IEnumerable<EmployeeDto> GetAll();
        (int totalCount, IEnumerable<EmployeeDto> items) Get(int? pageIndex = null, int? pageSize = null);
        ImportResultDto Import(IEnumerable<EmployeeDto> e);
        void Dispose();
    }
}