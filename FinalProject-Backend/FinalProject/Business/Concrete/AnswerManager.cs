using System;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete.DTOs;
using Entities.Models;

namespace Business.Concrete
{
  public class AnswerManager : IAnswerService
  {
    IAnswerDao _answerDao;

    public AnswerManager(IAnswerDao answerDao)
    {
      _answerDao = answerDao;
    }


    public IResult Add(Answer answer)
    {
      _answerDao.Add(answer);
      return new SuccessResult("Yanıt Eklendi");
    }


    public IResult Delete(Answer answer)
    {
      _answerDao.Delete(answer);
      return new SuccessResult();
    }


    public IDataResult<List<Answer>> GetAll()
    {
      return new SuccessDataResult<List<Answer>>(_answerDao.GetAll());
    }


    public IDataResult<List<Answer>> GetAnswersByQuestionId(int questionId)
    {
      return new SuccessDataResult<List<Answer>>(_answerDao.GetAll(x => x.QuestionId == questionId));
    }

    public IDataResult<List<AnswersResponseDto>> GetAnswersResponseByQuestionId(int questionId)
    {
      var answers = _answerDao.GetAll(x => x.QuestionId == questionId);
      var answerResponses = new List<AnswersResponseDto>();
      foreach (var answer in answers)
      {
        var answerResponse = new AnswersResponseDto();
        answerResponse.AnswerId = answer.AnswerId;
        answerResponse.AnswerText = answer.AnswerText;
        answerResponses.Add(answerResponse);
      }
      return new SuccessDataResult<List<AnswersResponseDto>>(answerResponses);
    }
  }
}

