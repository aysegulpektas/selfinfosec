using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs
{
  public class ResponseTokenModel
  {
    public string Token { get; set; }
    public string Username { get; set; }
    public DateTime Expiration { get; set; }
    public string UserRole {get;set;}
  }
}
