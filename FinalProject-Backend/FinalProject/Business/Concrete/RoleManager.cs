using System;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;

namespace Business.Concrete
{
  public class RoleManager : IRoleService
  {
    IRoleDao _roleDao;

    public RoleManager(IRoleDao roleDao)
    {
      _roleDao = roleDao;
    }

    [Authorize(Roles = "ADMIN")]
    public IResult Add(Role role)
    {
      _roleDao.Add(role);
      return new SuccessResult();
    }

    [Authorize(Roles = "ADMIN")]
    public IResult Delete(Role role)
    {
      _roleDao.Delete(role);
      return new SuccessResult();
    }


    public IDataResult<List<Role>> GetAll()
    {
      return new SuccessDataResult<List<Role>>(_roleDao.GetAll());
    }


    public IDataResult<Role> GetByRoleId(int id)
    {
      return new SuccessDataResult<Role>(_roleDao.Get(x => x.RoleId == id));
    }
  }
}

