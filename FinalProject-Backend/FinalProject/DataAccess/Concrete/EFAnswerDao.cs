using System;
using Core.Database.EntityFramework;
using DataAccess.Abstract;
using Entities.Models;

namespace DataAccess.Concrete
{
    public class EFAnswerDao : EFCrudOperations<Answer,AppDbContext>,IAnswerDao
        
    {
        public EFAnswerDao()
        {
        }
    }
}

