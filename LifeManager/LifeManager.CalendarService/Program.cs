using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using LifeManager.CalendarService.Services;
using LifeManager.Data.Entities;
using LifeManager.Data.Repositories;
using LifeManager.Messages.Calendar;
using LifeManager.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NServiceBus;

namespace LifeManager.CalendarService
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()        
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var config = builder.Build();
            var endpointConfiguration = new EndpointConfiguration("LifeManager.Calendar");
            endpointConfiguration.UseTransport<RabbitMQTransport>().ConnectionString(config["RabbitMqConnectionString"])
                .UseConventionalRoutingTopology();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.LicensePath(config["NServiceBusLicense"]);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CalendarEvent, CalendarEventModel>().ReverseMap();
                cfg.CreateMap<Person, PersonModel>().ReverseMap();
            });
            

            var diBuilder = new ContainerBuilder();
            diBuilder.RegisterInstance(new MongoClient())
                .SingleInstance();
            diBuilder.RegisterType<CalendarRepository>()
                .As<ICalendarRepository>()
                .SingleInstance();
            diBuilder.RegisterType<Services.CalendarService>()
                .As<ICalendarService>()
                .SingleInstance();
            var container = diBuilder.Build();
            endpointConfiguration.UseContainer<AutofacBuilder>(customizations =>
            {
                customizations.ExistingLifetimeScope(container);
            });

            try
            {
                var endpointInstance = await Endpoint.Start(endpointConfiguration)
                    .ConfigureAwait(false);                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            string input = string.Empty;

            while (input != "EXIT")
            {
                input = Console.ReadLine();
            }
            
        }
    }
}
