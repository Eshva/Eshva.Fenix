#region Usings

using System;

#endregion

namespace Eshva.OpenApiAndMongoDb.Models.ProductLimitPage
{
  public abstract class Participant
  {
    public bool IsBorrower;
    public Guid ParticipantId;
  }
}