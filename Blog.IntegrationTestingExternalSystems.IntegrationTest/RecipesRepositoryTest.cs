using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Blog.IntegrationTestingExternalSystems.IntegrationTest
{
    [TestFixture]
    public class RecipesRepositoryTest
    {
        private CosmosDbRepositoryTestContext _cosmosDbRepositoryTestContext;
        private RecipesRepository _repository;

        [SetUp]
        public async Task SetUp()
        {
            _cosmosDbRepositoryTestContext = new CosmosDbRepositoryTestContext();
            await _cosmosDbRepositoryTestContext.SetUpAsync();

            _repository = new RecipesRepository(Options.Create(_cosmosDbRepositoryTestContext.GetCosmosConfiguration()));
        }

        [TearDown]
        public async Task TearDown()
        {
            await _cosmosDbRepositoryTestContext.TearDownAsync();
        }

        [Test]
        public async Task WhenStoringARecipe_ThenItCanBeReadBack()
        {
            // Arrange
            var expected = new Recipe("my Recipe");

            // Act
            await _repository.AddAsync(expected);
            var actual = await _repository.GetByIdAsync(expected.id);

            // Assert
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [Test]
        public void WhenAnRecipeIsRequested_AndItDoesNotExist_ThenItThrowsRecipeNotFoundException()
        {
            // Act
            AsyncTestDelegate act = async () => await _repository.GetByIdAsync(Guid.NewGuid());

            //Assert
            Assert.ThrowsAsync<RecipeNotFoundException>(act);
        }
    }
}