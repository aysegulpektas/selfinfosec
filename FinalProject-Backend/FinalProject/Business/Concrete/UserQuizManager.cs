using System;
using Abstract;
using Business.Abstract;
using Core.Utilities.Results;
using Models;

namespace Concrete
{
    public class UserQuizManager : IUserQuizService
    {
        IUserQuizDao _userQuizDao;
        IQuestionGroupService _questionGroupService;
        public UserQuizManager(IUserQuizDao userQuizDao, IQuestionGroupService questionGroupService)
        {
            _userQuizDao = userQuizDao;
            _questionGroupService = questionGroupService;
        }
        public IResult EndQuiz(string userId, int questionGroupId)
        {
            var getQuiz = _userQuizDao.GetAll(x => x.UserId == userId && x.QuestionGroupId == questionGroupId);
            if (getQuiz.Count > 0)
            {
                var quiz = getQuiz[0];
                quiz.IsCompleted = true;
                _userQuizDao.Update(quiz);
                return new SuccessResult();
            }
            return new ErrorResult();

        }

        public IDataResult<bool> IsFinished(string userId, int questionGroupId)
        {
            var getQuiz = _userQuizDao.GetAll(x => x.UserId == userId && x.QuestionGroupId == questionGroupId && x.IsCompleted == true);
            if (getQuiz.Count > 0)
            {
                return new SuccessDataResult<bool>(true);
            }
            else
            {
                return new SuccessDataResult<bool>(false);
            }
        }

        public IDataResult<bool> IsJoined(string userId, int questionGroupId)
        {
            var getQuiz = _userQuizDao.GetAll(x => x.UserId == userId && x.QuestionGroupId == questionGroupId);
            if (getQuiz.Count > 0)
            {
                return new SuccessDataResult<bool>(true);
            }
            else
            {
                return new SuccessDataResult<bool>(false);
            }
        }

        public IResult JoinQuiz(string userId, int questionGroupId)
        {
            if(IsJoined(userId,questionGroupId).Data == false){
                var response = StartQuiz(userId,questionGroupId);
                return response;
            }else{
                if(IsFinished(userId,questionGroupId).Data == false){
                    return new SuccessResult();
                }else{
                    return new ErrorResult();
                }
            }
        }

        public IResult StartQuiz(string userId, int questionGroupId)
        {
            if (IsJoined(userId, questionGroupId).Data == false)
            {
                var articleId = _questionGroupService.Get(questionGroupId).Data.ArticleId;
                var userQuizModel = new UserQuiz(0, articleId, questionGroupId, userId, false);
                _userQuizDao.Add(userQuizModel);
                return new SuccessResult();
            }
            return new ErrorResult();

        }
    }
}