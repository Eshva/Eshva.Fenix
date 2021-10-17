#region Usings

using Eshva.OpenApiAndMongoDb.EndToEndTests.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#endregion

namespace Eshva.OpenApiAndMongoDb.EndToEndTests
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
      var configuration = context.Configuration;
      services.AddSingleton(configuration.GetSection(MongoConnectionConfiguration.Section).Get<MongoConnectionConfiguration>());
      services.AddSingleton(configuration.GetSection(DocumentCollectionsConfiguration.Section).Get<DocumentCollectionsConfiguration>());
    }

    public void ConfigureHost(IHostBuilder hostBuilder) =>
      hostBuilder
        .ConfigureAppConfiguration(
          configurationBuilder =>
            configurationBuilder
              .AddJsonFile(@"appsettings.json")
              .AddEnvironmentVariables("SERVICE_"));
  }
}
