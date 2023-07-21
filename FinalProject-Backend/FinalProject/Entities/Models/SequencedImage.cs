using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstarct;
using Entities.Models;

namespace Models
{
    public class SequencedImage:IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SequencedImageId { get; set; }
        public int ArticleId { get; set; }
        public virtual Article? Article {get;set;}
        public int Sequence { get; set; } //Resmin sırası
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int ShowType {get; set;} //Tıklayınca kayan resim, Tek tek gösterim vb
    }
}