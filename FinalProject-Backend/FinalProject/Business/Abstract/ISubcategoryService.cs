using System;
using Core.Utilities.Results;
using Entities.Models;

namespace Business.Abstract
{
	public interface ISubcategoryService
	{
        IResult Add(Subcategory subcategory);
        IResult Delete(Subcategory subcategory);
        IDataResult<List<Subcategory>> GetAll();
        IDataResult<List<Subcategory>> GetAllByCategoryId(int categoryId);
        IDataResult<Subcategory> GetById(int id);

    }
}

