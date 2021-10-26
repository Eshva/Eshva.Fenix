#region Usings

using System;
using System.Threading.Tasks;
using Eshva.Fenix.BusinessUserBff.Application;
using Eshva.Fenix.BusinessUserBff.Service.Bootstrapping;
using Eshva.Fenix.BusinessUserBff.Models.ProductLimitPage;
using MongoDB.Driver;

#endregion

namespace Eshva.Fenix.BusinessUserBff.Service.Infrastructure
{
  public class MongoProductLimitRevisionsStorage : IProductLimitRevisionsStorage
  {
    public MongoProductLimitRevisionsStorage(
      MongoClient mongoClient,
      MongoConnectionConfiguration mongoConnectionConfiguration,
      DocumentCollectionsConfiguration documentCollectionsConfiguration)
    {
      _mongoClient = mongoClient;
      _databaseName = mongoConnectionConfiguration.DatabaseName;
      _productLimitRevisionPages = documentCollectionsConfiguration.ProductLimitRevisionPages;
    }

    public Task<ProductLimitRevisionPageDto> GetById(Guid productLimitRevisionId)
    {
      var database = _mongoClient.GetDatabase(_databaseName);
      var collection = database.GetCollection<ProductLimitRevisionPageDto>(_productLimitRevisionPages);
      return collection.Find(document => document.Id.Equals(productLimitRevisionId)).SingleOrDefaultAsync();
    }

    public Task Store(ProductLimitRevisionPageDto productLimitRevisionPageDto) => throw new NotImplementedException();

    private readonly string _databaseName;
    private readonly MongoClient _mongoClient;
    private readonly string _productLimitRevisionPages;
  }
}
