using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = CreateHostBuilder(args);
await host.RunConsoleAsync();
return Environment.ExitCode;

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
        {
            builder.AddEnvironmentVariables();
        })
        .ConfigureServices((context, services) =>
        {
            services.AddHostedService<Worker>();
            services.AddSingleton<IKeyvaultClient, KeyvaultClient>();
        });


