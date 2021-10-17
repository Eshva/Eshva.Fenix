#region Usings

using Newtonsoft.Json;

#endregion

namespace Eshva.OpenApiAndMongoDb.Models.ProductLimitPage
{
  public class OtherOrganizationParticipant : Participant
  {
    [JsonRequired]
    public string Name;
  }
}
