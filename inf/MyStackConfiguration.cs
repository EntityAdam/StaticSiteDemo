public class MyStackConfiguration
{
    private const string Organization = "ea";
    private const string Application = "staticappdemo";
    private const string Environment = "dev";
    public string ResourceGroupName => $"{Organization}-{Application}-{Environment}-rg";
    public string StaticSiteName => $"{Organization}-{Application}-{Environment}-web";
    public string KeyVaultUri => $"https://{Organization}{Application}{Environment}kvtinf.vault.azure.net/";
    public string TenantId => "b848e0f6-95bd-4f24-a1dd-3b6c3c7813e1";
    public string Branch { get; set; } = "main";
    public string Location { get; set; } = "eastus2";
    public string FunctionsFolder { get; set; } = @"src/Functions";
    public string AppFolder { get; set; } = @"src/Client";

    //Issue: Oryx built the app folder but was unable to determine the location of the app artifacts. Please specify the app artifact location.
    public string AppBuildFolder { get; set; } = @"wwwroot";
    public string RepositoryUrl { get; set; } = "https://github.com/entityadam/StaticSiteDemo";
    public string? RepositoryToken { get; set; }
}
