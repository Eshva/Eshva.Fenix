#region Usings

using System;
using System.Threading.Tasks;
using Eshva.Fenix.BusinessUserBff.Models.ProductLimitPage;

#endregion

namespace Eshva.Fenix.BusinessUserBff.Application
{
  public interface IProductLimitRevisionsStorage
  {
    Task<ProductLimitRevisionPageDto> GetById(Guid productLimitRevisionId);

    Task Store(ProductLimitRevisionPageDto productLimitRevisionPageDto);
  }
}
