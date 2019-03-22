using AutoMapper;
using MyApp.Core.Commands.Contracts;
using MyApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyApp.Core.Commands
{
    public class SetAddressCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public SetAddressCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            var employeeId = int.Parse(inputArgs[0]);
            var address = string.Join(" ", inputArgs.Skip(1));

            var employee = this.context.Employees.FirstOrDefault(e => e.Id == employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Employee does not exist");
            }

            employee.Address = address;
            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}'s Address is set to \"{address}\"";
        }
    }
}
