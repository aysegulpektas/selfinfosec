using System;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;

namespace Business.Concrete
{
  public class SubcategoryManager : ISubcategoryService
  {
    ISubcategoryDao _subcategoryDao;

    public SubcategoryManager(ISubcategoryDao subcategoryDao)
    {
      _subcategoryDao = subcategoryDao;
    }

    [Authorize(Roles = "ADMIN")]
    public IResult Add(Subcategory subcategory)
    {
      _subcategoryDao.Add(subcategory);
      return new SuccessResult();
    }

    [Authorize(Roles = "ADMIN")]
    public IResult Delete(Subcategory subcategory)
    {
      _subcategoryDao.Delete(subcategory);
      return new SuccessResult();
    }


    public IDataResult<List<Subcategory>> GetAll()
    {
      return new SuccessDataResult<List<Subcategory>>(_subcategoryDao.GetAll());
    }


    public IDataResult<List<Subcategory>> GetAllByCategoryId(int categoryId)
    {
      return new SuccessDataResult<List<Subcategory>>(_subcategoryDao.GetAll(x => x.CategoryId == categoryId));
    }


    public IDataResult<Subcategory> GetById(int id)
    {
      return new SuccessDataResult<Subcategory>(_subcategoryDao.Get(x => x.SubcategoryId == id));
    }
  }
}

