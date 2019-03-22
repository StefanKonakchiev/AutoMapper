using AutoMapper;
using MyApp.Core.Commands.Contracts;
using MyApp.Core.ViewModels;
using MyApp.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MyApp.Core.Commands
{
    public class SetBirthdayCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public SetBirthdayCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            var employeeId = int.Parse(inputArgs[0]);
            var date = inputArgs[1];

            var employee = this.context.Employees.FirstOrDefault(e => e.Id == employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Employee does not exist");
            }

            DateTime dt;
            DateTime.TryParseExact(date, "dd-MM-yyyy", null, DateTimeStyles.None, out dt);

            if (dt.Year == 0001 || dt.Year < 1900)
            {
                throw new ArgumentException("Invalid date");
            }

            employee.Birthday = dt;
            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}'s birthday is set to {inputArgs[1]}";
        }
    }
}
