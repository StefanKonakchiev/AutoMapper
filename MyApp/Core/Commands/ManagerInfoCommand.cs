﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Core.Commands.Contracts;
using MyApp.Core.ViewModels;
using MyApp.Data;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyApp.Core.Commands
{
    public class ManagerInfoCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public ManagerInfoCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            var managerId = int.Parse(inputArgs[0]);

            Employee manager = this.context.Employees
                .Include(m => m.ManagedEmployees)
                .FirstOrDefault(x => x.Id == managerId);

            if (manager == null)
            {
                throw new ArgumentException("Manager does not exist");
            }

            var managerDto = this.mapper.CreateMappedObject<ManagerDto>(manager);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{managerDto.FirstName} {managerDto.LastName} | Employees:" +
                $"{managerDto.ManagedEmployees.Count}");

            foreach (var managedEmployee in managerDto.ManagedEmployees)
            {
                sb.AppendLine($"{managedEmployee.FirstName} {managedEmployee.LastName} " +
                    $"- ${managedEmployee.Salary}");
            }

            return sb.ToString();
        }
    }
}
