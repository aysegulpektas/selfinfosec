using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstarct;
using Entities.Models;

namespace Models
{
    public class UserQuiz : IEntity
    {
        public UserQuiz()
        {
            
        }
        public UserQuiz(int userQuizId,int articleId,int questionGroupId,string userId,bool isCompleted)
        {
            UserQuizId = userQuizId;
            ArticleId = articleId;
            QuestionGroupId = questionGroupId;
            UserId = userId;
            IsCompleted = isCompleted;
            StartDate = DateTime.UtcNow;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserQuizId { get; set; }
        public int ArticleId { get; set; }
        public virtual Article? Article {get;set;}
        public int QuestionGroupId { get; set; }
        public virtual QuestionGroup? QuestionGroup {get;set;}
        public string UserId { get; set; }
        public virtual User? User {get;set;}
        public bool IsCompleted { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate {get;set;}

    }
}