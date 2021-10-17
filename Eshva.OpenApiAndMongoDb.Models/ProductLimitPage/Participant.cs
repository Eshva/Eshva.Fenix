#region Usings

using System;
using System.Runtime.Serialization;
using JsonKnownTypes;
using Newtonsoft.Json;

#endregion

namespace Eshva.OpenApiAndMongoDb.Models.ProductLimitPage
{
  [KnownType(typeof(OtherOrganizationParticipant))]
  [KnownType(typeof(PrivateIndividualParticipant))]
  [JsonConverter(typeof(JsonKnownTypesConverter<Participant>))]
  // [JsonKnownType(typeof(OtherOrganizationParticipant), nameof(OtherOrganizationParticipant))]
  // [JsonKnownType(typeof(PrivateIndividualParticipant), nameof(PrivateIndividualParticipant))]
  public abstract class Participant
  {
    public bool IsBorrower;
    public Guid ParticipantId;
  }
}
