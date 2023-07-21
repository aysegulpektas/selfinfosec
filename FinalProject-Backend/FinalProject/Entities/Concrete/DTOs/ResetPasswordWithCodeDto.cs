using System;

namespace Entities.Concrete.DTOs
{
    public class ResetPasswordWithCodeDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }
    }
}