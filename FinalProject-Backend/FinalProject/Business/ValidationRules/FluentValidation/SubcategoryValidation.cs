using System;
using System.Text.RegularExpressions;
using Entities.Concrete.DTOs;
using Entities.Models;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class SubcategoryValidation : AbstractValidator<Subcategory>
    {
		public SubcategoryValidation()
		{
            RuleFor(p => p.SubcategoryName).NotEmpty();
            RuleFor(p => p.SubcategoryName).Must(OnlyLetter).WithMessage("Alt kategori adında yalnızca büyük ve küçük harfler kullanılabilir");
            RuleFor(p => p.SubcategoryName).Length(0, 50).WithMessage("Maksimum uzunluk 50 karakter olabilir");
        }

        private bool OnlyLetter(string str = "")
        {
            try
            {
                return Regex.IsMatch(str, @"^[a-zA-ZçöşğüÜĞŞıİÇÖ ]+$");
            }
            catch
            {
                return false;
            }
        }
    }
}

