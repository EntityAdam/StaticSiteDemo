using System.Threading.Tasks;

public interface IKeyvaultClient
{
    Task<string> FetchSecretFromKeyvault(string keyvaultUri, string secretKey, string? tenantId = default);
}
