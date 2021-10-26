#region Usings

using Newtonsoft.Json;

#endregion

namespace Eshva.Fenix.BusinessUserBff.Models.ProductLimitPage
{
  public class PrivateIndividualParticipant : Participant
  {
    [JsonRequired]
    public string FamilyName;

    [JsonRequired]
    public string FirstName;
  }
}
