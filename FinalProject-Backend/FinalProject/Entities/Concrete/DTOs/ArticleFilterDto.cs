using System;
namespace Entities.Concrete.DTOs
{
    public class ArticleFilterDto
    {
        public int[] Subcategories { get; set; }
        public string Title { get; set; }
    }
}
