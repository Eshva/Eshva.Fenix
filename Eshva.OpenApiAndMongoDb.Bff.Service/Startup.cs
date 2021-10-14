#region Usings

using Eshva.OpenApiAndMongoDb.Application;
using Eshva.OpenApiAndMongoDb.Bff.Service.Infrastructure;
using Eshva.OpenApiAndMongoDb.Models.ProductLimitPage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using NJsonSchema;
using NJsonSchema.Converters;
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
      services.AddMvc(options => options.EnableEndpointRouting = false)
        .AddNewtonsoftJson(
          options =>
          {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
            options.SerializerSettings.Converters.Add(new JsonInheritanceConverter(typeof(Participant), "$type"));
          });
      services.AddControllers();
      services.AddTransient<GetProductLimitPageDataById>();
      services.AddTransient<IProductLimitRevisionsStorage, MongoDbProductLimitRevisionsStorage>();
      services.AddOpenApiDocument(
        settings =>
        {
          settings.DocumentName = "pages";
          settings.Title = "Business-user SPA pages data API";

          var participantSchema = JsonSchema.FromType<Participant>();
          participantSchema.Discriminator = "$type";

          settings.SchemaGenerator.Settings.TypeMappers.Add(new ObjectTypeMapper(typeof(Participant), participantSchema));
          settings.SchemaGenerator.Settings.TypeMappers.Add(
            new ObjectTypeMapper(
              typeof(LimitType),
              new JsonSchema
              {
                Properties =
                {
                  { nameof(LimitType.Revolving), new JsonSchemaProperty { IsRequired = true } },
                  { nameof(LimitType.ProductLimitType), new JsonSchemaProperty { IsRequired = true } }
                }
              }));
        });
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
