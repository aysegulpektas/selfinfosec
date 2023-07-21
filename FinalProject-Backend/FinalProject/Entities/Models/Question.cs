using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstarct;

namespace Entities.Models
{
  public class Question : IEntity
  {
    public Question()
    {
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int QuestionId { get; set; }
    public int QuestionType { get; set; }
    public string QuestionText { get; set; }
    public string AddedUser { get; set; }
    public User? User {get;set;}
    public DateTime AddedDate { get; set; }
    public int QuestionGroupId { get; set; }
    public virtual QuestionGroup? QuestionGroup {get;set;}


    }
}

