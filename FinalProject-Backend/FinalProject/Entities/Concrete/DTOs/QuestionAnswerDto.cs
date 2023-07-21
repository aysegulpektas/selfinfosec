using System;
namespace Entities.Concrete.DTOs
{
  public class QuestionAnswerDto
  {
    public int QuestionId { get; set; }
    public string QuestionText { get; set; }
    public List<AnswersResponseDto> Answers { get; set; }
    public QuestionAnswerDto(int questionId,string questionText,List<AnswersResponseDto> answers)
    {
      QuestionId = questionId;
      QuestionText = questionText;
      Answers = answers;
    }
  }
}

