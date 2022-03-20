# Install Azure Functions Core Tools

> https://github.com/Azure/azure-functions-core-tools#installing

# Add Reference Packages

```xml
<PackageReference Include="Microsoft.Azure.Functions.Extensions" Version="1.1.0" />
<PackageReference Include="Microsoft.Extensions.Http" Version="6.0.0" />
<PackageReference Include="Microsoft.NET.Sdk.Functions" Version="4.0.1" />
```

# Add Startup.cs

```cs
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Functions.Startup))]
namespace Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
        }
    }
}
```