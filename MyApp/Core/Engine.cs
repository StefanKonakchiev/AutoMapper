﻿using MyApp.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace MyApp.Core
{
    public class Engine : IEngine
    {
        private readonly IServiceProvider provider;

        public Engine(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public void Run()
        {
            while (true)
            {
                string[] inputArgs = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

                var commandIntepreter = this.provider.GetService<ICommandInterpreter>();

                try
                {
                    string result = commandIntepreter.Read(inputArgs);
                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    //throw;
                }
            }



        }
    }
}
