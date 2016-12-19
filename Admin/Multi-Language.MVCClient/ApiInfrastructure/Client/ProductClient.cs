using Multi_Language.MVCClient.ApiInfrastructure.ApiModels;

namespace Multi_Language.MVCClient.ApiInfrastructure.Client
{
    using System;
    using System.Threading.Tasks;
    using Multi_language.ApiHelper.Client;
    using ApiModels;
    using Models;
    using Responses;

    //public class ProductClient : ClientBase, IProductClient
    //{
    //    private const string ProductUri = "api/product";
    //    private const string ProductsUri = "api/products";
    //    private const string IdKey = "id";

    //    public ProductClient(IApiClient apiClient) : base(apiClient)
    //    {
    //    }

    //    public async Task<CreateProductResponse> CreateProduct(ProductViewModel product)
    //    {
    //        var apiModel = new ProductApiModel
    //        {
    //            CreatedOn = DateTime.Now,
    //            Name = product.Name,
    //            Description = product.Description
    //        };
    //        var createProductResponse = await PostEncodedContentWithSimpleResponse<CreateProductResponse, ProductApiModel>(ProductsUri, apiModel);
    //        return createProductResponse;
    //    }

    //    public async Task<ProductResponse> GetProduct(int productId)
    //    {
    //        var idPair = IdKey.AsPair(productId.ToString());
    //        return await GetJsonDecodedContent<ProductResponse, ProductApiModel>(ProductUri, idPair);
    //    }
    //}
}