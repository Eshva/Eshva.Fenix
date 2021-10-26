#region Usings

using Newtonsoft.Json;

#endregion

namespace Eshva.Fenix.BusinessUserBff.Models.ProductLimitPage
{
  public class OtherOrganizationParticipant : Participant
  {
    [JsonRequired]
    public string Name;
  }
}
