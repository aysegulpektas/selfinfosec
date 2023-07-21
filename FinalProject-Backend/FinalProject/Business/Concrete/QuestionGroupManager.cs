using System;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Models;

namespace Business.Concrete
{
	public class QuestionGroupManager : IQuestionGroupService
	{
		IQuestionGroupDao _questionGroupDao;

		public QuestionGroupManager(IQuestionGroupDao questionGroupDao)
		{
			_questionGroupDao = questionGroupDao;
		}

        public IDataResult<QuestionGroup> Add(QuestionGroup questionGroup)
        {
            var addedQuestionGroup = _questionGroupDao.Add(questionGroup);
            return new SuccessDataResult<QuestionGroup>(addedQuestionGroup);
        }

        public IResult Delete(QuestionGroup questionGroup)
        {
            _questionGroupDao.Delete(questionGroup);
            return new SuccessResult();
        }

        public IDataResult<QuestionGroup> Get(int QuestionGroupId)
        {
            return new SuccessDataResult<QuestionGroup>(_questionGroupDao.Get(x => x.QuestionGroupId == QuestionGroupId));
        }

        public IDataResult<List<QuestionGroup>> GetAll()
        {
            return new SuccessDataResult<List<QuestionGroup>>(_questionGroupDao.GetAll());
        }

        public IDataResult<List<QuestionGroup>> GetAllByArticleId(int articleId)
        {
            return new SuccessDataResult<List<QuestionGroup>>(_questionGroupDao.GetAll(x => x.ArticleId == articleId));
        }

        public IResult Update(QuestionGroup questionGroup)
        {
            _questionGroupDao.Update(questionGroup);
            return new SuccessResult();
        }
    }
}

