using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstarct;

namespace Entities.Models
{
  public class Answer : IEntity
  {
    public Answer()
    {
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AnswerId { get; set; }
    public string AnswerText { get; set; }
    public int QuestionId { get; set; }
    public virtual Question? Question {get;set;}
    public bool IsTrue { get; set; }

  }
}

