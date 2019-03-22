using AutoMapper;
using MyApp.Core.Commands.Contracts;
using MyApp.Core.ViewModels;
using MyApp.Data;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MyApp.Core.Commands
{
    public class AddEmployeeCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public AddEmployeeCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            string firstName = inputArgs[0];
            string lastName = inputArgs[1];
            decimal salary = decimal.Parse(inputArgs[2]);

            var regexString = new Regex("^[a-zA-Z]{3,30}$");

            if (!regexString.IsMatch(firstName))
            {
                throw new ArgumentException("First name cannot contain any special characters nad has to be between 3 and 30 symbols");
            }

            if (!regexString.IsMatch(lastName))
            {
                throw new ArgumentException("Last name cannot contain any special characters nad has to be between 3 and 30 symbols");
            }

            if (salary < 0)
            {
                throw new ArgumentException("Salary must be a positive value");
            }

            var employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary
            };

            this.context.Employees.Add(employee);
            this.context.SaveChanges();

            var employeeDto = this.mapper.CreateMappedObject<EmployeeDto>(employee);

            string result = $"Registered succesfully {employeeDto.FirstName} {employeeDto.LastName} - {employeeDto.Salary}";
            return result;
        }
    }

}
