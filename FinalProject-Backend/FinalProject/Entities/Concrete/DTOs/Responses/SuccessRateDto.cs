using System;

namespace Responses
{
    public class SuccessRateDto
    {
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public int QuestionGroupId { get; set; }
        public string QuestionGroupText { get; set; }
        public int TrueAnswersCount {get;set;}
        public int WrongAnswersCount {get;set;}
        public double SuccessRate { get; set; }
    }
}