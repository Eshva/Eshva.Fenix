#region Usings

using System;
using System.Threading.Tasks;
using Eshva.OpenApiAndMongoDb.Application;
using Eshva.OpenApiAndMongoDb.Models;
using Eshva.OpenApiAndMongoDb.Models.ProductLimitPage;
using Microsoft.AspNetCore.Http;
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
    [ProducesResponseType(typeof(ProductLimitRevisionPageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProductLimitRevisionPageDto>> GetPageDataById(Guid productLimitRevisionId)
    {
      try
      {
        var pageData = await _getProductLimitPageDataByIdUseCase.Execute(productLimitRevisionId);
        return Ok(pageData);
      }
      catch
      {
        return StatusCode(StatusCodes.Status500InternalServerError, new Error { Message = "Server error.", Code = 787989 });
      }
    }

    private readonly GetProductLimitPageDataById _getProductLimitPageDataByIdUseCase;
  }
}
