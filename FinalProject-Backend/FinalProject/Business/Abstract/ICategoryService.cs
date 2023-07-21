using System;
using Core.Utilities.Results;
using Entities.Models;

namespace Business.Abstract
{
	public interface ICategoryService
	{
        IResult Add(Category category);
        IResult Delete(Category category);
        IDataResult<List<Category>> GetAll();
        IDataResult<Category> GetById(int id);
    }
}

