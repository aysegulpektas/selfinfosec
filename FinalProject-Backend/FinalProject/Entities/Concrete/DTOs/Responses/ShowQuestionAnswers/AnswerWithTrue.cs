using System;

namespace ShowQuestionAnswers
{
    public class AnswerWithTrue
    {
        public AnswerWithTrue(int answerId, string answerText, bool isTrue)
        {
            AnswerId = answerId;
            AnswerText = answerText;
            IsTrue = isTrue;
        }

        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public bool IsTrue { get; set; }

    }
}