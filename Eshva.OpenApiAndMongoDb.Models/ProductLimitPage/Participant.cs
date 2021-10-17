#region Usings

using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using NJsonSchema.Converters;

#endregion

namespace Eshva.OpenApiAndMongoDb.Models.ProductLimitPage
{
  [KnownType(typeof(OtherOrganizationParticipant))]
  [KnownType(typeof(PrivateIndividualParticipant))]
  [JsonConverter(typeof(JsonInheritanceConverter), "$type")]
  public abstract class Participant
  {
    [JsonRequired]
    public bool IsBorrower;

    [JsonRequired]
    public Guid ParticipantId;
  }
}
