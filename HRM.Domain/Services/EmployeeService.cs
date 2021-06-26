using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HRM.DataAccessEf.Entities;
using HRM.DataAccessEf.Interfaces;
using HRM.Domain.DTO;
using HRM.Domain.Exceptions;

namespace HRM.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public void Create(EmployeeDto e)
        {
            var employee = _create(_mapper.Map<Employee>(e));
            _uow.Employees.Add(employee);
            _uow.Save();
        }

        public void Update(EmployeeDto e)
        {
            var employee = _uow.Employees.GetById(e.Id);

            _update(employee, _mapper.Map<Employee>(e));
            _uow.Employees.Update(employee);
            _uow.Save();
        }

        public void Remove(Guid id)
        {
            var employee = _uow.Employees.GetById(id);

            _uow.Employees.Remove(employee);
            _uow.Save();
        }

        public EmployeeDto GetById(Guid id)
        {
            var employee = _uow.Employees.GetById(id);

            return _mapper.Map<EmployeeDto>(employee);
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var employee = _uow.Employees.GetAll();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employee);
        }

        public (int totalCount, IEnumerable<EmployeeDto> items) Get(int? pageIndex = null, int? pageSize = null)
        {
            var (totalCount, employees) = _uow.Employees.Get(pageIndex, pageSize);

            return (totalCount, _mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }

        public ImportResultDto Import(IEnumerable<EmployeeDto> importItems)
        {
            var res = new ImportResultDto();


            var existEmployeesNumberMap = _uow.Employees.GetAll(i => i.Number != null)
                .ToDictionary(i => i.Number, i => i);

            var items = _mapper.Map<List<Employee>>(importItems);

            var duplicateNumbers = items
                .Where(i => i.Number != null)
                .GroupBy(i => i.Number)
                .Where(g => g.Count() > 1)
                .ToDictionary(i => i.Key, i => i);

            res.Errors = duplicateNumbers
                .SelectMany(i => i.Value.Select(j => $"{j.Name} {j.Number} - Duplicate Personnel Number")).ToList();

            foreach (var i in items.Where(i => i.Number == null || !duplicateNumbers.ContainsKey(i.Number)))
            {
                try
                {
                    if (i.Number != null && existEmployeesNumberMap.TryGetValue(i.Number, out var existEmployee))
                    {
                        _update(existEmployee, i);
                        _uow.Employees.Update(existEmployee);
                        res.Modified.Add($"{i.Name} {i.Number}");
                    }
                    else
                    {
                        var employee = _create(i);
                        _uow.Employees.Add(employee);
                        res.Created.Add($"{i.Name} {i.Number}");
                    }
                }
                catch (NotModifiedException _)
                {
                }
                catch (Exception e)
                {
                    res.Errors.Add($"{i.Name} {i.Number} {e.GetBaseException().Message}");
                }
            }

            _uow.Save();

            return res;
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

        private void _validate(Employee e)
        {
            if (e.IsFullTime && e.Number == null)
                throw new Exception("Full Time employee must have Personnel Number");

            if (!e.IsFullTime && e.Number != null)
                throw new Exception("Freelance employee can`t have Personnel Number");
        }

        private void _update(Employee currentItem, Employee newItem)
        {
            _validate(newItem);

            if (newItem.Number != null &&
                _uow.Employees.GetAll(i => i.Number == newItem.Number && i.Id != newItem.Id).Any())
                throw new Exception("Personnel Number is already exist");

            if (currentItem.Name == newItem.Name
                && currentItem.Number == newItem.Number
                && currentItem.Birthday == newItem.Birthday
                && currentItem.Gender == newItem.Gender
                && currentItem.IsFullTime == newItem.IsFullTime)
            {
                throw new NotModifiedException("");
            }


            currentItem.Name = newItem.Name;
            currentItem.Number = newItem.Number;
            currentItem.Gender = newItem.Gender;
            currentItem.Birthday = newItem.Birthday;
            currentItem.IsFullTime = newItem.IsFullTime;
            currentItem.ModifiedTime = DateTime.UtcNow;
        }

        private Employee _create(Employee e)
        {
            _validate(e);

            if (e.Number != null && _uow.Employees.GetAll(i => i.Number == e.Number).Any())
                throw new Exception("Personnel Number is already exist");

            var employee = _mapper.Map<Employee>(e);
            employee.Id = Guid.NewGuid();
            employee.CreatedTime = DateTime.UtcNow;

            return employee;
        }
    }
}