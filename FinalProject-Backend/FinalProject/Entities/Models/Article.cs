using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstarct;

namespace Entities.Models
{
  public class Article : IEntity
  {
    public Article()
    {

    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ArticleId { get; set; }
    [MaxLength(50)]
    public string ArticleTitle { get; set; }
    public string ArticleDescription {get;set;}
    public string ArticleFilePath { get; set; }
    public string ContentType { get; set; }
    public string AddedUser { get; set; }
    public virtual User? User {get;set;}
    public DateTime AddedDate { get; set; }
    public int SubcategoryId { get; set; }
    public virtual Subcategory? Subcategory {get;set;}
    public string LanguageCode { get; set; }

  }
}

