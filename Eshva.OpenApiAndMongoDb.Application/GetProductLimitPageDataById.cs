#region Usings

using System;
using System.Threading.Tasks;
using Eshva.OpenApiAndMongoDb.Models.ProductLimitPage;

#endregion

namespace Eshva.OpenApiAndMongoDb.Application
{
  public class GetProductLimitPageDataById
  {
    public GetProductLimitPageDataById(IProductLimitRevisionsStorage storage)
    {
      _storage = storage;
    }

    public Task<ProductLimitRevisionPageDto> Execute(Guid productLimitRevisionId) => _storage.GetById(productLimitRevisionId);

    private readonly IProductLimitRevisionsStorage _storage;
  }
}
