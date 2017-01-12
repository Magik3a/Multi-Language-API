namespace Multi_Language.DataApi.Tasks
{
    public interface IBearerTokenExpirationTask
    {
        bool BearerTokenExpired(string token);
    }
}