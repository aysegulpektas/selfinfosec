using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstarct;

namespace Entities.Models
{
    public class Subcategory : IEntity
    {
        public Subcategory()
        {
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubcategoryId { get; set; }
        [MaxLength(255)]
        public string SubcategoryName { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

    }
}

