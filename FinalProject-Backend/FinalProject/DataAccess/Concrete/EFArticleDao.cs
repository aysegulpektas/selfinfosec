using System;
using Core.Database.EntityFramework;
using DataAccess.Abstract;
using Entities.Models;

namespace DataAccess.Concrete
{
    public class EFArticleDao : EFCrudOperations<Article, AppDbContext>, IArticleDao
    {
        public EFArticleDao()
        {
        }
    }
}

