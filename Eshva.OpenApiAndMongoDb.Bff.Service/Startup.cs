#region Usings

using Eshva.OpenApiAndMongoDb.Application;
using Eshva.OpenApiAndMongoDb.Bff.Service.Infrastructure;
using Eshva.OpenApiAndMongoDb.Models.ProductLimitPage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NJsonSchema;
using NJsonSchema.Converters;
using NJsonSchema.Generation;
using NJsonSchema.Generation.TypeMappers;

#endregion

namespace Eshva.OpenApiAndMongoDb.Bff.Service
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddMvc(options => options.EnableEndpointRouting = false)
        .AddNewtonsoftJson(options => ConfigureJsonSerialization(options.SerializerSettings));
      services.AddControllers();
      services.AddTransient<GetProductLimitPageDataById>();
      services.AddTransient<IProductLimitRevisionsStorage, MongoDbProductLimitRevisionsStorage>();
      services.AddOpenApiDocument(
        document =>
        {
          document.DocumentName = "pages";
          document.Title = "Business-user SPA pages data API";
          document.Version = "v1";
          document.Description = "Сервис получения данных для страниц SPA бизнес-пользователя.";

          var participantSchema = JsonSchema.FromType<Participant>();
          participantSchema.Discriminator = "$type";
          document.SchemaGenerator.Settings.TypeMappers.Add(new ObjectTypeMapper(typeof(Participant), participantSchema));

          var schemaGeneratorSettings = new JsonSchemaGeneratorSettings{SerializerSettings = new JsonSerializerSettings()};
          ConfigureJsonSerialization(schemaGeneratorSettings.SerializerSettings);

          var limitTypeSchema = JsonSchema.FromType<LimitType>(schemaGeneratorSettings);
          limitTypeSchema.Properties[nameof(LimitType.Revolving)].IsRequired = true;
          limitTypeSchema.Properties[nameof(LimitType.ProductLimitType)].IsRequired = true;
          document.SchemaGenerator.Settings.TypeMappers.Add(new ObjectTypeMapper(typeof(LimitType), limitTypeSchema));
        });
    }

    private static void ConfigureJsonSerialization(JsonSerializerSettings serializerSettings)
    {
      serializerSettings.Converters.Add(new StringEnumConverter());
      serializerSettings.Converters.Add(new JsonInheritanceConverter(typeof(Participant), "$type"));
    }

    public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
    {
      application.UseMvc();
      application.UseHttpsRedirection();
      application.UseRouting();
      application.UseAuthorization();
      application.UseOpenApi(settings => settings.Path = "/api/{documentName}/swagger.json");
    }
  }
}
