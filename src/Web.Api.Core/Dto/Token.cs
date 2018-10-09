 

namespace Web.Api.Core.Dto
{
  public sealed class Token
  {
    public string Id { get; }
    public string AccessToken { get; }
    public int ExpiresIn { get; }

    public Token(string id, string accessToken, int expiresIn)
    {
      Id = id;
      AccessToken = accessToken;
      ExpiresIn = expiresIn;
    }
  }
}
