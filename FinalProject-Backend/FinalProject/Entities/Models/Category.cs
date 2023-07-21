using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstarct;

namespace Entities.Models
{
  public class Category : IEntity
  {
    public Category()
    {
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }
    [MaxLength(255)]
    public string CategoryName { get; set; }
    [MaxLength(255)]
    public string CategoryDescription { get; set; }

  }
}

