namespace Multi_Language.MVCClient.ApiInfrastructure.ApiModels
{
    using Multi_language.ApiHelper.Model;

    public class RegisterApiModel : ApiModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}