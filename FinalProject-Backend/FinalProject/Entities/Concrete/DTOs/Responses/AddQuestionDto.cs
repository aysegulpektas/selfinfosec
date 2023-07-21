using System;
namespace Entities.Concrete.DTOs.Responses
{
    public class AddQuestionDto
    {
        public int QuestionType { get; set; }
        public int QuestionGroupId { get; set; }
        public string QuestionText { get; set; }

    }
}

