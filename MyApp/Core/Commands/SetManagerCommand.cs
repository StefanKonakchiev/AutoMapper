﻿using AutoMapper;
using MyApp.Core.Commands.Contracts;
using MyApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Core.Commands
{
    public class SetManagerCommand : ICommand
    {
        private readonly MyAppContext context;

        public SetManagerCommand(MyAppContext context)
        {
            this.context = context;
        }

        public string Execute(string[] inputArgs)
        {
            int employeeId = int.Parse(inputArgs[0]);
            int managerId = int.Parse(inputArgs[1]);

            var employee = this.context.Employees.Find(employeeId);
            var manager = this.context.Employees.Find(managerId);

            if (employee == null)
            {
                throw new ArgumentException("Employee does not exist");
            }

            if (manager == null)
            {
                throw new ArgumentException("Manager does not exist");
            }

            employee.Manager = manager;
            this.context.SaveChanges();

            return "Command completed succesfully!";
        }
    }
}
