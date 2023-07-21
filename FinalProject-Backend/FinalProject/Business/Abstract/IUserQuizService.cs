using System;
using Core.Utilities.Results;

namespace Abstract
{
    public interface IUserQuizService
    {
        public IResult JoinQuiz(string userId,int questionGroupId);
        public IResult StartQuiz(string userId,int questionGroupId);
        public IResult EndQuiz(string userId,int questionGroupId);
        public IDataResult<bool> IsJoined(string userId,int questionGroupId);
        public IDataResult<bool> IsFinished(string userId,int questionGroupId);
    }
}