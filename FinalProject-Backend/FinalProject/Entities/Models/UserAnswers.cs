using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstarct;

namespace Entities.Models
{
  public class UserAnswers : IEntity
  {
    public UserAnswers()
    {
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserAnswersId { get; set; }
    public string UserId { get; set; }
    public virtual User? User {get;set;}
    public int QuestionId { get; set; }
    public virtual Question? Question {get;set;}
    public int AnswersId { get; set; }
    public virtual Answer? Answer {get;set;}

  }
}

