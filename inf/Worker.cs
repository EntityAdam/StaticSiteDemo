using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
internal class Worker : IHostedService
{
    //private readonly IKeyvaultClient keyvaultClient;
    private readonly IHostEnvironment environment;
    private readonly IHostApplicationLifetime hostApplicationLifetime;

    public Worker(
        //IKeyvaultClient service,
        IHostEnvironment environment,
        IHostApplicationLifetime hostApplicationLifetime)
    {
        //this.keyvaultClient = service;
        this.environment = environment;
        this.hostApplicationLifetime = hostApplicationLifetime;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("> Starting Application");
        Console.WriteLine($"Environment: {environment.EnvironmentName}");

        //await keyvaultClient.FetchConnectionStringsFromKeyvault();
        await Pulumi.Deployment.RunAsync<MyStack>();

        hostApplicationLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("> Stopping Application");
        return Task.CompletedTask;
    }
}
