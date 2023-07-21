using System;
using Core.Database.EntityFramework;
using DataAccess.Abstract;
using Entities.Models;

namespace DataAccess.Concrete
{
	public class EFQuestionGroupDao : EFCrudOperations<QuestionGroup, AppDbContext>, IQuestionGroupDao
    {
		public EFQuestionGroupDao()
		{
		}
	}
}

