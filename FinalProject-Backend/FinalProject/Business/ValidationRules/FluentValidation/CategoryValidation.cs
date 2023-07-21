using System;
using System.Text.RegularExpressions;
using Entities.Concrete.DTOs;
using Entities.Models;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class CategoryValidation : AbstractValidator<Category>
    {
		public CategoryValidation()
		{
            RuleFor(p => p.CategoryName).NotEmpty();
            RuleFor(p => p.CategoryName).Must(OnlyLetter).WithMessage("Kategori adında yalnızca büyük ve küçük harfler kullanılabilir");
            RuleFor(p => p.CategoryName).Length(0, 50).WithMessage("Maksimum uzunluk 50 karakter olabilir");

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

