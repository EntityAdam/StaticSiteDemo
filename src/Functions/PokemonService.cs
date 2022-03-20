using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace EntityAdam.PokemonService
{
    public class PokemonService
    {
        private HttpClient httpClient { get; set; }
        public PokemonService(IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient();
        }

        [FunctionName("PokemonService")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var pokemon = await httpClient.GetFromJsonAsync<Pokemon>("https://pokeapi.co/api/v2/pokemon/squirtle");
            return new OkObjectResult(pokemon);
        }
    }

    public class Pokemon
    {
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public Sprites Sprites {get;set;}

    }
    public class Sprites
    {
        [JsonPropertyName("front_default")]
        public string Front {get;set;}
    }
}