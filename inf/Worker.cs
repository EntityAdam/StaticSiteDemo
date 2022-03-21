using Microsoft.Extensions.Hosting;
using Pulumi;
using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.Web;
using Pulumi.AzureNative.Web.Inputs;
using System;
using System.Threading;
using System.Threading.Tasks;
internal class Worker : IHostedService
{
    private readonly IHostEnvironment environment;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly IKeyvaultClient keyvaultClient;

    public Worker(IHostEnvironment environment, IHostApplicationLifetime hostApplicationLifetime, IKeyvaultClient keyvaultClient)
    {
        this.environment = environment;
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.keyvaultClient = keyvaultClient;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("> Starting Application");
        Console.WriteLine($"Environment: {environment.EnvironmentName}");

        var configuration = new MyStackConfiguration();

        var githubToken = await keyvaultClient.FetchSecretFromKeyvault(configuration.KeyVaultUri, "pulumi-github-token", configuration.TenantId);

        await Pulumi.Deployment.RunAsync(() =>
        {
            var resourceGroup = new ResourceGroup("rg", new ResourceGroupArgs()
            {
                ResourceGroupName = configuration.ResourceGroupName,
                Location = configuration.Location
            });
            var staticSite = new StaticSite("web", new StaticSiteArgs()
            {
                Name = configuration.StaticSiteName,
                Branch = configuration.Branch,
                BuildProperties = new StaticSiteBuildPropertiesArgs()
                {
                    ApiLocation = configuration.FunctionsFolder,
                    AppLocation = configuration.AppFolder,
                    OutputLocation = configuration.AppBuildFolder
                },
                Location = configuration.Location,
                RepositoryToken = githubToken,
                RepositoryUrl = configuration.RepositoryUrl,
                ResourceGroupName = configuration.ResourceGroupName,
                Sku = new SkuDescriptionArgs()
                {
                    Name = "Free",
                    Tier = "Free"
                }
            }, new CustomResourceOptions() { DependsOn = { resourceGroup } });
        });
        hostApplicationLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("> Stopping Application");
            return Task.CompletedTask;
        }
    }
