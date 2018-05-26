using System;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using LifeManager.Data.Entities;
using LifeManager.Data.Repositories;
using LifeManager.ListsService.Services;
using LifeManager.Models;
using Microsoft.Extensions.Configuration;
using NServiceBus;
using MongoDB.Driver;

namespace LifeManager.ListsService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var config = builder.Build();
            var endpointConfiguration = new EndpointConfiguration("LifeManager.Lists");
            endpointConfiguration.UseTransport<RabbitMQTransport>().ConnectionString(config["RabbitMqConnectionString"])
                .UseConventionalRoutingTopology();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.LicensePath(config["NServiceBusLicense"]);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<List, ListModel>().ReverseMap();                
            });


            var diBuilder = new ContainerBuilder();
            var mongoClient = new MongoClient();
            var database = mongoClient.GetDatabase("lists");
            diBuilder.RegisterInstance(database);
            diBuilder.RegisterType<ListRepository>()
                .As<IListRepository>()
                .SingleInstance();
            diBuilder.RegisterType<Services.ListService>()
                .As<IListService>()
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
