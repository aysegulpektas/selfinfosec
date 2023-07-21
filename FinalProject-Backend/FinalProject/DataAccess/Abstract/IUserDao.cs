using System;
using System.Linq.Expressions;
using Core.Database.Interfaces;
using Entities.Concrete.DTOs;
using Entities.Models;

namespace DataAccess.Abstract
{
    public interface IUserDao : ICrudBase<User>
    {

    }
}

