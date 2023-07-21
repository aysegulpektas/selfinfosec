using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Tools
{
  public class JWTCreator
  {
    public string UserId { get; }
    public string Role { get; }
    public DateTime Expiration { get; }
    public Dictionary<string,string> AdditionalClaims { get; }
    private string SecretKey;
    private string Issuer;
    public JWTCreator(string secretKey,string issuer, string userId, string role, DateTime expiration, Dictionary<string, string> additionalClaims)
    {
      UserId = userId;
      Role = role;
      Expiration = expiration;
      AdditionalClaims = additionalClaims;
      SecretKey = secretKey;
      Issuer = issuer;
    }
    public string GenerateToken()
    {
      var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
      var claims = new List<Claim>();
      var tokenHandler = new JwtSecurityTokenHandler();
      claims.Add(new Claim(ClaimTypes.NameIdentifier, UserId));
      if(Role != null)
      {
        claims.Add(new Claim(ClaimTypes.Role, Role));
      }
      if(AdditionalClaims != null)
      {
        foreach (var additionalClaim in AdditionalClaims)
        {
          claims.Add(new Claim(additionalClaim.Key, additionalClaim.Value));
        }
      }

      var tokenDescriptor = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(claims),
        Expires = Expiration,
        Issuer = Issuer,
        SigningCredentials = credentials
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}
