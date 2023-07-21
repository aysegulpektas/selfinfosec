using System;
using Core.Database.EntityFramework;
using DataAccess.Abstract;
using Entities.Models;

namespace DataAccess.Concrete
{
    public class EFCategoryDao : EFCrudOperations<Category, AppDbContext>, ICategoryDao
    {
        public EFCategoryDao()
        {
        }
    }
}

