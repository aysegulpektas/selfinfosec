using System;
using Core.Utilities.Results;
using Entities.Concrete.DTOs;
using Entities.Models;

namespace Business.Abstract
{
  public interface IAnswerService
  {
    IResult Add(Answer answer);
    IResult Delete(Answer answer);
    IDataResult<List<Answer>> GetAll();
    IDataResult<List<Answer>> GetAnswersByQuestionId(int questionId);
    IDataResult<List<AnswersResponseDto>> GetAnswersResponseByQuestionId(int questionId);

  }
}

