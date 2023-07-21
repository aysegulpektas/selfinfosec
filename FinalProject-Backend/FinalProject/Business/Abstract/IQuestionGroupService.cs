using System;
using Core.Utilities.Results;
using Entities.Models;

namespace Business.Abstract
{
	public interface IQuestionGroupService
	{
        IDataResult<QuestionGroup> Add(QuestionGroup questionGroup);
        IResult Delete(QuestionGroup questionGroup);
        IResult Update(QuestionGroup questionGroup);
        IDataResult<QuestionGroup> Get(int QuestionGroupId);
        IDataResult<List<QuestionGroup>> GetAll();
        IDataResult<List<QuestionGroup>> GetAllByArticleId(int articleId);
    }
}

