using System;
using FluentValidation;
using System.Runtime.ConstrainedExecution;
using Entities.Models;
using System.Text.RegularExpressions;

namespace Business.ValidationRules.FluentValidation
{
	public class UserValidation : AbstractValidator<User>
    {
		public UserValidation()
		{
           
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.FirstName).Must(OnlyLetter).WithMessage("Adınızda yalnızca harfler kullanılabilir");
            RuleFor(p => p.Lastname).NotEmpty();
            RuleFor(p => p.Lastname).Must(OnlyLetter).WithMessage("Soyadınızda yalnızca harfler kullanılabilir");
            RuleFor(p => p.UserName).NotEmpty();
            RuleFor(p => p.UserName).MinimumLength(8).WithMessage("Kullanıcı adınız minimum 8 karakter olmalıdır");
            RuleFor(p => p.UserName).Must(LetterNumberUnderScoreAndPoint).WithMessage("Kullanıcı adında yalnızca harfler sayılar alt tire ve nokta kullanılabilir.");

        }


        private bool LetterNumberUnderScoreAndPoint(string str = "")
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



        private bool OnlyLetter(string str= "")
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

