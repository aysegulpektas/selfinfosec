using System;
using System.Text.RegularExpressions;
using Entities.Concrete.DTOs;
using Entities.Models;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class UserRegisterModelValidation : AbstractValidator<UserRegisterModel>
    {
		public UserRegisterModelValidation()
		{
            RuleFor(p => p.Password).NotEmpty();
            RuleFor(p => p.Password).MinimumLength(8).WithMessage("Şifreniz minimum 8 karakter olmalıdır");
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.FirstName).Must(OnlyLetter).WithMessage("Adınızda yalnızca harfler kullanılabilir");
            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.LastName).Must(OnlyLetter).WithMessage("Soyadınızda yalnızca harfler kullanılabilir");
            RuleFor(p => p.Username).NotEmpty();
            RuleFor(p => p.Username).MinimumLength(8).WithMessage("Kullanıcı adınız minimum 8 karakter olmalıdır");
            RuleFor(p => p.Username).Must(LetterNumberUnderScoreAndPoint).WithMessage("Kullanıcı adında yalnızca harfler sayılar alt tire ve nokta kullanılabilir.");
        }


        private bool LetterNumberUnderScoreAndPoint(string str = "" ) 
        {
            try
            {
                return Regex.IsMatch(str, @"^[a-zA-Z0-9_.]+$");
            }
            catch
            {
                return false;
            }
        }


        private bool OnlyLetter(string str= "" )
        {
            try
            {
                return Regex.IsMatch(str, @"^[a-zA-ZçöşğüÜĞŞıİÇÖ]+$");
            }
            catch
            {
                return false;
            }
        }
    }
}

