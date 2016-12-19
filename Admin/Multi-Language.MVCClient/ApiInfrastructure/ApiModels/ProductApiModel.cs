namespace Multi_Language.MVCClient.ApiInfrastructure.ApiModels
{
    using System;
    using Multi_language.ApiHelper.Model;

    public class ProductApiModel : ApiModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}