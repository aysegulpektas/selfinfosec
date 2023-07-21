using System;
using Core.Utilities.Results;
using Entities.Concrete.DTOs;
using Entities.Concrete.DTOs.Responses;
using Entities.Models;
using ShowQuestionAnswers;

namespace Business.Abstract
{
  public interface IQuestionService
  {
    IDataResult<Question> Add(Question question);
    IResult Delete(Question question);
    IDataResult<Question> GetByQuestionId(int id);
    IDataResult<List<Question>> GetAll();
    IDataResult<Question> AddQuestionWithAddedUser(AddQuestionDto addQuestionDto);
    IDataResult<List<QuestionAnswerDto>> GetQuestionsByQuestionGroupId(int questionGroupId);
    IDataResult<List<QuestionWithAnswerDto>> GetQuestionsWithAnswersByQuestionGroupId(int questionGroupId);

  }
}

