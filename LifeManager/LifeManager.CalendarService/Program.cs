using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using LifeManager.CalendarService.Services;
using Microsoft.Extensions.Configuration;
using NServiceBus;

namespace LifeManager.CalendarService
{
    class Program
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
            endpointConfiguration.LicensePath(config["NServiceBusLicense"]);

            var diBuilder = new ContainerBuilder();
            diBuilder.RegisterInstance(new Services.CalendarService())
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
