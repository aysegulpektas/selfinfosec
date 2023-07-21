using System;
using Core.Utilities.Results;
using Entities.Models;
using Responses;

namespace Business.Abstract
{
	public interface IUserAnswersService
	{
        IResult Add(UserAnswers userAnswers);
        IResult Delete(UserAnswers userAnswers);
        IDataResult<List<UserAnswers>> GetAll();
        IDataResult<UserAnswers> GetById(int id);
        IDataResult<List<UserAnswers>> GetByUserId(string userId);
        IDataResult<UserAnswers> GetByUserAndAnswerId(string userId, int answerId);
        IDataResult<List<UserAnswers>> GetUserAnswersByAnswerId(int answerId);
        IDataResult<bool> AnswerQuestion(int question, int answer);
        IDataResult<List<SuccessRateDto>> GetSuccessRate(string userId);
    }
}

