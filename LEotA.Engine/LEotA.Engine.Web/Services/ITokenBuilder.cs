namespace LEotA.Engine.Web.Services
{
    public interface ITokenBuilder
    {
        string BuildToken(string username);
    }
}