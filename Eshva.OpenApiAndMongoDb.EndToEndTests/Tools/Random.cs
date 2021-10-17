#region Usings

using RandomStringCreator;

#endregion

namespace Eshva.OpenApiAndMongoDb.EndToEndTests.Tools
{
  public static class Random
  {
    public static string String(int length = 10) => StringCreator.Get(length);

    private static readonly StringCreator StringCreator = new(@"abcdefghijklmnopqrstuvwxyz");
  }
}
