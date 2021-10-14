#region Usings

using System;

#endregion

namespace Eshva.OpenApiAndMongoDb.Models.ProductLimitPage
{
  public class ProductLimitRevisionPageDto
  {
    public Guid Id;
    public LimitType LimitType;
    public Participant[] Participants;
  }
}
