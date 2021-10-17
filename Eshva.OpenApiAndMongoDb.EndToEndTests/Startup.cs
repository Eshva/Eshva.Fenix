#region Usings

using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Eshva.OpenApiAndMongoDb.EndToEndTests
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton(new MongoConnectionConfiguration("mongodb://localhost:40001", "bff-database", "eshva", @"changeit"));
      services.AddTransient<MongoTestContext>();
    }
  }
}
