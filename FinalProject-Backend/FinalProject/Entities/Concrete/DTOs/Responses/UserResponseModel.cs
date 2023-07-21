using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs.Responses
{
  public class UserResponseModel
  {
    public string UserId { get; set; }

    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string Lastname { get; set; }

    public string Email { get; set; }
  }
}
