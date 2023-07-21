using System;
using Core.Database.EntityFramework;
using DataAccess.Abstract;
using Entities.Models;

namespace DataAccess.Concrete
{
    public class EFUserDao : EFCrudOperations<User, AppDbContext>, IUserDao
    {
        public EFUserDao()
        {
        }
    }
}

