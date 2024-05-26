using Blazorise;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject3;


    [CollectionDefinition("BlazoriseServices")]
    public class BlazoriseServicesCollection : ICollectionFixture<BlazoriseServicesFixture> { }
    public class BlazoriseServicesFixture
    {
        public BlazoriseServicesFixture()
        {
             var serviceCollection = new ServiceCollection();
            serviceCollection.AddBlazorise();
        }
     public ServiceProvider ServiceProvider { get; }
    }