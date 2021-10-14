#region Usings

using System;
using System.Threading.Tasks;
using Eshva.OpenApiAndMongoDb.Models.ProductLimitPage;

#endregion

namespace Eshva.OpenApiAndMongoDb.Application
{
  public interface IProductLimitRevisionsStorage
  {
    Task<ProductLimitRevisionPageDto> GetById(Guid productLimitRevisionId);

    Task Store(ProductLimitRevisionPageDto productLimitRevisionPageDto);
  }
}
