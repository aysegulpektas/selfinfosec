using System;
using Core.Database.EntityFramework;
using DataAccess.Abstract;
using Entities.Models;

namespace DataAccess.Concrete
{
    public class EFQuestionDao : EFCrudOperations<Question, AppDbContext>, IQuestionDao
    {
        public EFQuestionDao()
        {
        }
    }
}

