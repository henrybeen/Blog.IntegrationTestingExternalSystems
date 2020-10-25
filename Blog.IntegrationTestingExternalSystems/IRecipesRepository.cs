using System;
using System.Threading.Tasks;

namespace Blog.IntegrationTestingExternalSystems
{
    public interface IRecipesRepository
    {
        Task AddAsync(Recipe recipe);
        Task<Recipe> GetByIdAsync(Guid id);
    }
}
