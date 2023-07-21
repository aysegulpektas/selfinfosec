using System;
namespace Entities.Concrete.DTOs
{
  public class AddArticleDto
  {
    public string ArticleTitle { get; set; }
    public string ArticleDescription { get; set; }
    public int SubcategoryId { get; set; }
    public string ArticleContent { get; set; }
    public string ContentType { get; set; }
    public string LanguageCode { get; set; }
  }
}

