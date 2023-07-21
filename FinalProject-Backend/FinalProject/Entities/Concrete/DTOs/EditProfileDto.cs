using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete.DTOs
{
	public class EditProfileDto
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}

