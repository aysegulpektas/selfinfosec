using System;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;

namespace Business.Concrete
{
  public class CategoryManager : ICategoryService
  {
    ICategoryDao _categoryDao;

    public CategoryManager(ICategoryDao categoryDao)
    {
      _categoryDao = categoryDao;
    }

    [Authorize(Roles = "ADMIN")]
    public IResult Add(Category category)
    {
      _categoryDao.Add(category);
      return new SuccessResult();
    }
    [Authorize(Roles = "ADMIN")]
    public IResult Delete(Category category)
    {
      _categoryDao.Delete(category);
      return new SuccessResult();
    }

    public IDataResult<List<Category>> GetAll()
    {
      return new SuccessDataResult<List<Category>>(_categoryDao.GetAll());
    }

    public IDataResult<Category> GetById(int id)
    {
      return new SuccessDataResult<Category>(_categoryDao.Get(x => x.CategoryId == id));
    }
  }
}

