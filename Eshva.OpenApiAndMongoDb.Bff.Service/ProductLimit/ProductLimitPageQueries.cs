#region Usings

using System;
using System.Threading.Tasks;
using Eshva.OpenApiAndMongoDb.Application;
using Eshva.OpenApiAndMongoDb.Models.ProductLimitPage;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace Eshva.OpenApiAndMongoDb.Bff.Service.ProductLimit
{
  [Route("/api/pages/product-limit")]
  [ApiController]
  public class ProductLimitPageQueries : ControllerBase
  {
    public ProductLimitPageQueries(GetProductLimitPageDataById getProductLimitPageDataByIdUseCase)
    {
      _getProductLimitPageDataByIdUseCase = getProductLimitPageDataByIdUseCase;
    }

    [HttpGet("{productLimitRevisionId:guid}")]
    public async Task<ActionResult<ProductLimitRevision>> GetPageDataById(Guid productLimitRevisionId)
    {
      var pageData = await _getProductLimitPageDataByIdUseCase.Execute(productLimitRevisionId);
      return Ok(pageData);
    }

    private readonly GetProductLimitPageDataById _getProductLimitPageDataByIdUseCase;
  }
}
