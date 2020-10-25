namespace Blog.IntegrationTestingExternalSystems
{
    public class CosmosConfiguration
    {
        public string EndpointUrl { get; set; }
        public string AuthorizationKey  { get; set; }
        public string RecipeDatabaseName  { get; set; }
        public string RecipeContainerName  { get; set; }
    }
}