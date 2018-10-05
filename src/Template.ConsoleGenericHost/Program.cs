using System;
using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutofacSerilogIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Template.ConsoleGenericHost
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                var host = new HostBuilder()
                           .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                           .ConfigureHostConfiguration(configHost =>
                           {
                               configHost.SetBasePath(Directory.GetCurrentDirectory());
                               configHost.AddJsonFile("hostsettings.json", optional: true);
                               //configHost.AddEnvironmentVariables(prefix: "PREFIX_");
                               configHost.AddCommandLine(args);
                           })
                           .ConfigureAppConfiguration(configApp =>
                           {
                               configApp.AddJsonFile("appsettings.json", optional: false);
                               //configApp.AddEnvironmentVariables(prefix: "PREFIX_");
                               configApp.AddCommandLine(args);
                           })
                           .ConfigureContainer<ContainerBuilder>(builder =>
                           {
                               builder.RegisterLogger();
                               builder.RegisterType<LifetimeEventsHostedService>().As<IHostedService>().SingleInstance();
                           })
                           .ConfigureServices((hostContext, services) =>
                           {
                           })
                           .UseSerilog((hostContext, configLogging) =>
                           {
                               configLogging.ReadFrom.Configuration(hostContext.Configuration);
                           })
                           .UseConsoleLifetime()
                           .Build();

                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Web Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
