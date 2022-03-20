using Pulumi;
using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.Web;
using Pulumi.AzureNative.Web.Inputs;

internal sealed class MyStack : Stack
{
    public MyStack()
    {
        var configuration = new MyStackConfiguration();
        var resourceGroup = new ResourceGroup(configuration.ResourceGroupName, new ResourceGroupArgs() 
        {
            Location = configuration.Location
        }); 
        var staticSite = new StaticSite(configuration.StaticSiteName, new StaticSiteArgs()
        {
            Branch = configuration.Branch,
            BuildProperties = new StaticSiteBuildPropertiesArgs()
            {
                ApiLocation = configuration.FunctionsFolder,
                AppLocation = configuration.AppFolder,
                OutputLocation = configuration.AppBuildFolder
            },
            Location = configuration.Location,
            RepositoryToken = configuration.RepositoryToken!,
            RepositoryUrl = configuration.RepositoryUrl,
            ResourceGroupName = configuration.ResourceGroupName,
            Sku = new SkuDescriptionArgs()
            {
                Name = "Free",
                Tier = "Free"
            }
        });
    }
}