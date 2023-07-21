using System;

namespace ShowQuestionAnswers
{
    public class QuestionWithAnswerDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<AnswerWithTrue> Answers { get; set; }
        public QuestionWithAnswerDto(int questionId, string questionText, List<AnswerWithTrue> answers)
        {
            QuestionId = questionId;
            QuestionText = questionText;
            Answers = answers;
        }
    }
}