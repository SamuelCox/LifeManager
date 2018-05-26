using System;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using LifeManager.Data.Entities;
using LifeManager.Data.Repositories;
using LifeManager.Models;
using LifeManager.PeopleService.Services;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NServiceBus;

namespace LifeManager.PeopleService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var config = builder.Build();
            var endpointConfiguration = new EndpointConfiguration("LifeManager.People");
            endpointConfiguration.UseTransport<RabbitMQTransport>().ConnectionString(config["RabbitMqConnectionString"])
                .UseConventionalRoutingTopology();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.LicensePath(config["NServiceBusLicense"]);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Person, PersonModel>().ReverseMap();
            });


            var diBuilder = new ContainerBuilder();
            var mongoClient = new MongoClient();
            var database = mongoClient.GetDatabase("people");
            diBuilder.RegisterInstance(database);
            diBuilder.RegisterType<PeopleRepository>()
                .As<IPeopleRepository>()
                .SingleInstance();
            diBuilder.RegisterType<Services.PeopleService>()
                .As<IPeopleService>()
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
