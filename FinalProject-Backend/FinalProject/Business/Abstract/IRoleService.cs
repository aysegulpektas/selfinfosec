using System;
using Core.Utilities.Results;
using Entities.Models;

namespace Business.Abstract
{
	public interface IRoleService
	{
        IResult Add(Role role);
        IResult Delete(Role role);
        IDataResult<Role> GetByRoleId(int id);
        IDataResult<List<Role>> GetAll();
    }
}

