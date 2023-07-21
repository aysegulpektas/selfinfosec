namespace API.ConfigurationModels
{
  public class JWTConfig
  {
    public string SigningKey { get; set; }
    public string Issuer { get; set; }
  }
}
