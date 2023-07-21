using System;
using Core.Database.EntityFramework;
using DataAccess.Abstract;
using Entities.Models;

namespace DataAccess.Concrete
{
    public class EFResetCodeDao : EFCrudOperations<ResetCode,AppDbContext>, IResetCodeDao
    {
        
    }
}