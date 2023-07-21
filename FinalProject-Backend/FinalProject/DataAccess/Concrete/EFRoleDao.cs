using System;
using Core.Database.EntityFramework;
using DataAccess.Abstract;
using Entities.Models;

namespace DataAccess.Concrete
{
    public class EFRoleDao : EFCrudOperations<Role, AppDbContext>, IRoleDao
    {
        public EFRoleDao()
        {
        }
    }
}

