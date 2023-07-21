using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete.DTOs.Responses
{
  public class ArticleDataModel
  {

    public int ArticleId { get; set; }
    [MaxLength(50)]
    public string ArticleTitle { get; set; }
    public string ArticleFilePath { get; set; }
    public string ArticleContent { get; set; }
    public string ContentType { get; set; }
    public string AddedUser { get; set; }
    public DateTime AddedDate { get; set; }
    public int SubcategoryId { get; set; }
    public string LanguageCode { get; set; }
  }
}

