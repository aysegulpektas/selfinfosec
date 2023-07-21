using System;
using Core.Database.EntityFramework;
using DataAccess.Abstract;
using Entities.Models;

namespace DataAccess.Concrete
{
    public class EFUserAnswersDao : EFCrudOperations<UserAnswers, AppDbContext>, IUserAnswersDao
    {
        public EFUserAnswersDao()
        {
        }
    }
}

