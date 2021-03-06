#region Usings

using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using NJsonSchema.Converters;

#endregion

namespace Eshva.Fenix.BusinessUserBff.Models.ProductLimitPage
{
  [KnownType(typeof(OtherOrganizationParticipant))]
  [KnownType(typeof(PrivateIndividualParticipant))]
  [BsonKnownTypes(typeof(OtherOrganizationParticipant), typeof(PrivateIndividualParticipant))]
  [JsonConverter(typeof(JsonInheritanceConverter), "$type")]
  public abstract class Participant
  {
    [JsonRequired]
    public bool IsBorrower;

    [JsonRequired]
    public Guid ParticipantId;
  }
}
