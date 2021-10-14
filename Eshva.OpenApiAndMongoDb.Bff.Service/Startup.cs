#region Usings

using Eshva.OpenApiAndMongoDb.Application;
using Eshva.OpenApiAndMongoDb.Bff.Service.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;

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
        .AddNewtonsoftJson(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter()); });
      services.AddControllers();
      services.AddTransient<GetProductLimitPageDataById>();
      services.AddTransient<IProductLimitRevisionsStorage, MongoDbProductLimitRevisionsStorage>();
      services.AddOpenApiDocument(
        settings =>
        {
          settings.DocumentName = "pages";
          settings.Title = "Business-user SPA pages data API";
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
