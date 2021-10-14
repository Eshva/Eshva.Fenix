#region Usings

using System;
using System.Threading.Tasks;
using Eshva.OpenApiAndMongoDb.Application;
using Eshva.OpenApiAndMongoDb.Models.ProductLimitPage;

#endregion

namespace Eshva.OpenApiAndMongoDb.Bff.Service.Infrastructure
{
  public class MongoDbProductLimitRevisionsStorage : IProductLimitRevisionsStorage
  {
    public Task<ProductLimitRevisionPageDto> GetById(Guid productLimitRevisionId)
    {
      var revision = new ProductLimitRevisionPageDto
      {
        Id = productLimitRevisionId,
        LimitType = new LimitType { Revolving = Revolving.Revolving, ProductLimitType = ProductLimitType.Overdraft },
        Participants = new Participant[]
        {
          new OtherOrganizationParticipant { ParticipantId = Guid.NewGuid(), IsBorrower = true, Name = "Miracle Inc." },
          new PrivateIndividualParticipant
            { ParticipantId = Guid.NewGuid(), IsBorrower = false, FirstName = "Merlin", FamilyName = "Bashirov" }
        }
      };

      return Task.FromResult(revision);
    }

    public Task Store(ProductLimitRevisionPageDto productLimitRevisionPageDto) => throw new NotImplementedException();
  }
}
