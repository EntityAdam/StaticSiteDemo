public class MyStackConfiguration
{
    private const string Organization = "entityadam";
    private const string Application = "blog";
    private const string Environment = "dev";
    public string ResourceGroupName => $"{Organization}-{Application}-{Environment}-rg";
    public string StaticSiteName => $"{Organization}-{Application}-{Environment}-web";
    public string Branch { get; set; } = "main";
    public string Location { get; set; } = "eastus";
    public string FunctionsFolder { get; set; } = "Functions";
    public string AppFolder { get; set; } = @"..\src\Client";
    public string AppBuildFolder { get; set; } = @"..\src\Client\bin\release\net6.0";
    public string RepositoryUrl { get; set; } = "https://github.com/entityadam/StaticSiteDemo";
    public string? RepositoryToken { get; set; }
}
