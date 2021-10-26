#region Usings

using Eshva.Fenix.BusinessUserBff.Application;
using Eshva.Fenix.BusinessUserBff.Service.Bootstrapping;
using Eshva.Fenix.BusinessUserBff.Service.Infrastructure;
using Eshva.Fenix.BusinessUserBff.Models.ProductLimitPage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NJsonSchema.Converters;

#endregion

namespace Eshva.Fenix.BusinessUserBff.Service
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton(_configuration.GetSection(MongoConnectionConfiguration.Section).Get<MongoConnectionConfiguration>());
      services.AddSingleton(_configuration.GetSection(DocumentCollectionsConfiguration.Section).Get<DocumentCollectionsConfiguration>());

      services
        .AddMvc(options => options.EnableEndpointRouting = false)
        .AddNewtonsoftJson(options => ConfigureJsonSerialization(options.SerializerSettings));
      services.AddControllers();
      services.AddTransient<IProductLimitRevisionsStorage, MongoProductLimitRevisionsStorage>();
      services.AddSingleton(new MongoClient(_configuration.GetSection("DocumentStorage")["ConnectionString"]));
      services.AddOpenApiDocument(
        document =>
        {
          document.DocumentName = "pages";
          document.Title = "Business-user SPA pages data API";
          document.Version = "v1";
          document.Description = "Сервис получения данных для страниц SPA бизнес-пользователя.";
        });

      services.AddTransient<GetProductLimitPageDataById>();
    }

    public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
    {
      application.UseMvc();
      application.UseRouting();
      application.UseAuthorization();
      application.UseOpenApi(settings => settings.Path = "/api/{documentName}/swagger.json");
    }

    private static void ConfigureJsonSerialization(JsonSerializerSettings serializerSettings)
    {
      serializerSettings.Converters.Add(new StringEnumConverter());
      serializerSettings.Converters.Add(new JsonInheritanceConverter(typeof(Participant), "$type"));
    }

    private readonly IConfiguration _configuration;
  }
}
