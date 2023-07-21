using System;
using Abstract;
using Core.Database.EntityFramework;
using DataAccess.Concrete;
using Models;

namespace Concrete
{
    public class EFUserQuizDao:EFCrudOperations<UserQuiz, AppDbContext>, IUserQuizDao
    {
        public EFUserQuizDao()
        {
            
        }
    }
}