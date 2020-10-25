using System;
using Newtonsoft.Json;

namespace Blog.IntegrationTestingExternalSystems
{
    public class Recipe
    {
        public Guid id { get; set; }

        public string Name { get; set; }

        public Recipe(string name)
        {
            id = Guid.NewGuid();
            Name = name;
        }

        private Recipe()
        {
            
        }
    }
}