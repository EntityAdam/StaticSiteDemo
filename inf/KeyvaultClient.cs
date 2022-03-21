using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

public class KeyvaultClient : IKeyvaultClient
{
    public async Task<string> FetchSecretFromKeyvault(string keyvaultUri, string secretKey, string? tenantId = default)
    {
        var keyvault = keyvaultUri;

        // known issue:
        // https://github.com/Azure/azure-cli/issues/11871?msclkid=c1d6a4e9a94311ec9d765a4dba3d58cc
        var options = new DefaultAzureCredentialOptions();
        if (!string.IsNullOrEmpty(tenantId)) 
        {
            options.VisualStudioTenantId = tenantId;
        }
        // end work-around

        var client = new SecretClient(new Uri(keyvault), new DefaultAzureCredential(options));
        var response = await client.GetSecretAsync(secretKey);
        return response.Value.Value;
    }
}