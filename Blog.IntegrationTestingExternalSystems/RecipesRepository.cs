using System;
using System.Linq;
using System.Threading.Tasks;
using Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Blog.IntegrationTestingExternalSystems
{
    public class RecipesRepository : IRecipesRepository
    {
        private readonly IOptions<CosmosConfiguration> _cosmosConfiguration;

        public RecipesRepository(IOptions<CosmosConfiguration> cosmosConfiguration)
        {
            _cosmosConfiguration = cosmosConfiguration;
        }

        public async Task AddAsync(Recipe recipe)
        {
            await GetContainer().UpsertItemAsync(recipe, new PartitionKey(recipe.id.ToString()));
        }

        public async Task<Recipe> GetByIdAsync(Guid id)
        {
            var query= new QueryDefinition("SELECT * FROM c WHERE c.id = @id")
                .WithParameter("@id", id.ToString());

            var results = await GetContainer().GetItemQueryIterator<Recipe>(query).ToArrayAsync();

            if (!results.Any())
            {
                throw new RecipeNotFoundException();
            }

            return results.Single();
        }

        private CosmosContainer GetContainer()
        {
            var client = new CosmosClient(
                _cosmosConfiguration.Value.EndpointUrl, 
                _cosmosConfiguration.Value.AuthorizationKey);

            return client.GetContainer(
                _cosmosConfiguration.Value.RecipeDatabaseName, 
                _cosmosConfiguration.Value.RecipeContainerName);
        }
    }
}