#region Usings

using System;
using System.Runtime.Serialization;

#endregion

namespace Eshva.OpenApiAndMongoDb.Models.ProductLimitPage
{
  [KnownType(typeof(OtherOrganizationParticipant))]
  [KnownType(typeof(PrivateIndividualParticipant))]
  public abstract class Participant
  {
    public bool IsBorrower;
    public Guid ParticipantId;
  }
}
