namespace Eshva.OpenApiAndMongoDb.EndToEndTests.Tools
{
  public class MongoFixture
  {
    public MongoFixture(MongoTestContext context)
    {
      Context = context;
    }

    public MongoTestContext Context { get; set; }
  }
}
