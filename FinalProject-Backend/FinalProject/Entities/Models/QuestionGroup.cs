using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstarct;
using Entities.Models;

namespace Entities.Models
{
    public class QuestionGroup : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionGroupId { get; set; }
        public int ArticleId { get; set; }
        public virtual Article? Article {get;set;}
        public string GroupTitle { get; set; }
        public bool UseForScore { get; set; }

    }
}

