using System;
using System.Security.Cryptography;

namespace Entities.Concrete.DTOs
{
	public class PasswordResetDto
	{
	   public string NewPassword { get; set; }
       public string OldPassword { get; set; }
    }
}

