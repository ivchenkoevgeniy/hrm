using System;
using System.Collections.Generic;
using AutoMapper;
using HRM.Controllers;
using HRM.DataAccessEf.Entities;
using HRM.DataAccessEf.Enums;
using HRM.DataAccessEf.Interfaces;
using HRM.Domain;
using HRM.Domain.DTO;
using HRM.Domain.Services;
using NUnit.Framework;
using Moq;
using Newtonsoft.Json;

namespace HRM.Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestGetAll()
        {
            // Arrange
            var mock = new Mock<IEmployeeService>();
            var e = new EmployeeDto()
            {
                Id = Guid.NewGuid(),
                Number = 1,
                Name = "Anna",
                Gender = Gender.Female.ToString(),
                Birthday = DateTime.Parse("1996-05-08").Date.ToString("MM/dd/yyyy"),
                IsFullTime = true
            };
            mock.Setup(a => a.GetAll()).Returns(new List<EmployeeDto> {e});
            var expected = JsonConvert.SerializeObject(new List<EmployeeDto> {e});
            var controller = new EmployeeController(mock.Object);

            // Act
            var actual = controller.GetData();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCreate1()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>();
                cfg.CreateMap<EmployeeDto, Employee>();
            });
            var mapper = new Mapper(config);

            var e1 = new Employee()
            {
                Id = Guid.NewGuid(),
                Number = 1,
                Name = "Anna1",
                Gender = Gender.Female,
                Birthday = DateTime.Parse("1996-05-08").Date,
                IsFullTime = true
            };

            var e2 = new EmployeeDto()
            {
                Id = Guid.NewGuid(),
                Number = 1,
                Name = "Anna2",
                Gender = Gender.Female.ToString(),
                Birthday = DateTime.Parse("1996-05-08").Date.ToString("MM/dd/yyyy"),
                IsFullTime = true
            };

            mock.Setup(a => a.Employees.GetAll(It.IsAny<Func<Employee, bool>>()))
                .Returns(new List<Employee> {e1});

            var employeeService = new EmployeeService(mock.Object, mapper);

            // Assert
            Assert.Throws<Exception>(() => { employeeService.Create(e2); });
        }

        [Test]
        public void TestCreate2()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>();
                cfg.CreateMap<EmployeeDto, Employee>();
            });
            var mapper = new Mapper(config);

            var e1 = new Employee()
            {
                Id = Guid.NewGuid(),
                Number = 1,
                Name = "Anna1",
                Gender = Gender.Female,
                Birthday = DateTime.Parse("1996-05-08").Date,
                IsFullTime = false
            };

            var e2 = new EmployeeDto()
            {
                Id = Guid.NewGuid(),
                Number = 1,
                Name = "Anna2",
                Gender = Gender.Female.ToString(),
                Birthday = DateTime.Parse("1996-05-08").Date.ToString("MM/dd/yyyy"),
                IsFullTime = true
            };

            mock.Setup(a => a.Employees.GetAll(It.IsAny<Func<Employee, bool>>()))
                .Returns(new List<Employee> {e1});

            var employeeService = new EmployeeService(mock.Object, mapper);

            // Assert
            Assert.Throws<Exception>(() => { employeeService.Create(e2); });
        }

        [Test]
        public void TestUpdate1()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>();
                cfg.CreateMap<EmployeeDto, Employee>();
            });
            var mapper = new Mapper(config);

            var e1 = new Employee()
            {
                Id = Guid.NewGuid(),
                Number = 1,
                Name = "Anna1",
                Gender = Gender.Female,
                Birthday = DateTime.Parse("1996-05-08").Date,
                IsFullTime = true
            };

            var e2 = new EmployeeDto()
            {
                Id = Guid.NewGuid(),
                Number = 1,
                Name = "Anna2",
                Gender = Gender.Female.ToString(),
                Birthday = DateTime.Parse("1996-05-08").Date.ToString("MM/dd/yyyy"),
                IsFullTime = true
            };

            mock.Setup(a => a.Employees.GetAll(It.IsAny<Func<Employee, bool>>()))
                .Returns(new List<Employee> {e1});

            var employeeService = new EmployeeService(mock.Object, mapper);

            // Assert
            Assert.Throws<Exception>(() => { employeeService.Update(e2); });
        }
    }
}