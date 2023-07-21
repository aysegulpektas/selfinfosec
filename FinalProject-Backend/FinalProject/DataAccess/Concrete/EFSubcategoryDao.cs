using System;
using Core.Database.EntityFramework;
using DataAccess.Abstract;
using Entities.Models;

namespace DataAccess.Concrete
{
    public class EFSubcategoryDao : EFCrudOperations<Subcategory, AppDbContext>, ISubcategoryDao
    {
        public EFSubcategoryDao()
        {
        }
    }
}

