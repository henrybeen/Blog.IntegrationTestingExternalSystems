using System;
using System.Threading.Tasks;
using Azure.Cosmos;
using NUnit.Framework;

namespace Blog.IntegrationTestingExternalSystems.IntegrationTest
{
    public class CosmosDbRepositoryTestContext
    {
        private CosmosConfiguration _configuration;
        private CosmosContainer _container;

        public async Task SetUpAsync()
        {
            _configuration = new CosmosConfiguration()
            {
                EndpointUrl = TestContext.Parameters["EndpointUrl"],
                AuthorizationKey = TestContext.Parameters["AuthorizationKey"],
                RecipeDatabaseName = TestContext.Parameters["RecipeDatabaseName"],
                RecipeContainerName = $"integrationtest-{Guid.NewGuid()}"
            };

            var cosmosclient = new CosmosClient(_configuration.EndpointUrl, _configuration.AuthorizationKey);
            var database = cosmosclient.GetDatabase(_configuration.RecipeDatabaseName);
            var containerResponse = await database.CreateContainerIfNotExistsAsync(_configuration.RecipeContainerName, "/id");
            _container = containerResponse.Container;
        }

        public async Task TearDownAsync()
        {
            await _container.DeleteContainerAsync();
        }

        public CosmosConfiguration GetCosmosConfiguration()
        {
            return _configuration;
        }
    }
}