using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Core;
using MyApp.Core.Contracts;
using MyApp.Data;
using System;

namespace MyApp
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            //database
            //command pattern
            //DI
            //DTOs
            //Service Layer


            IServiceProvider services = ConfigureServices();

            IEngine engine = new Engine(services);
            engine.Run();


            MapperConfiguration mapperConfiguration = new MapperConfiguration();

            //mapperConfiguration.Mapper.CreateMappedObject
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<MyAppContext>(db => 
                db.UseSqlServer("Server=.\\SQLEXPRESS;Database=MySpecialApp;Integrated Security=True"));

            serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>();
            serviceCollection.AddTransient<Mapper>();

            //Da si gi vidq tiq dvete
            //serviceCollection.AddSingleton<Mapper>();
            //serviceCollection.AddScoped<Mapper>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
