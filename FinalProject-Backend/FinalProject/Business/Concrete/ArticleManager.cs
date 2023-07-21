using System;
using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete.DTOs;
using Entities.Concrete.DTOs.Responses;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;

namespace Business.Concrete
{
  public class ArticleManager : IArticleService
  {
    IArticleDao _articleDao;
    IHttpContextHelperService _httpContextHelperService;
    IFileOperationsService _fileOperationsService;
    IMapper _mapper;
    string mainPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "articles" + Path.DirectorySeparatorChar;

    public ArticleManager(IArticleDao articleDao, IFileOperationsService fileOperationsService, IHttpContextHelperService httpContextHelperService, IMapper mapper)
    {
      _articleDao = articleDao;
      _fileOperationsService = fileOperationsService;
      _httpContextHelperService = httpContextHelperService;
      _mapper = mapper;

    }
    [Authorize(Roles ="ADMIN")]

    public IResult Add(AddArticleDto article)
    {
      var userId = _httpContextHelperService.GetUserId();
      var addedDate = DateTime.UtcNow;
      var addedArticle = new Article();

      var fileName = Guid.NewGuid().ToString("N") + ".html";
      _fileOperationsService.CreateFile(mainPath, fileName, article.ArticleContent);
      addedArticle.AddedUser = userId;
      addedArticle.AddedDate = addedDate;
      addedArticle.ArticleTitle = article.ArticleTitle;
      addedArticle.ArticleFilePath = fileName;
      addedArticle.ArticleDescription = article.ArticleDescription;
      addedArticle.ContentType = article.ContentType;
      addedArticle.SubcategoryId = article.SubcategoryId;
      addedArticle.LanguageCode = article.LanguageCode;

      _articleDao.Add(addedArticle);
      return new SuccessResult(Environment.CurrentDirectory);
    }

    [Authorize(Roles = "ADMIN")]
    public IResult Delete(int articleId)
    {
      var article = _articleDao.Get(x => x.ArticleId == articleId);
      _fileOperationsService.DeleteFile(mainPath, article.ArticleFilePath);
      _articleDao.Delete(article);
      return new SuccessResult();
    }
    private string GetLanguage(){
      var userLangCode = _httpContextHelperService.GetHeaders().FirstOrDefault(x=>x.Key == "user-language").Value ?? "tr";
      if(userLangCode != "tr" && userLangCode != "en"){
        userLangCode = "tr";
      }
      return userLangCode;
    }
    public IDataResult<List<Article>> GetAllArticles(ArticleFilterDto filter = null,bool showAll = false)
    {
      var tempArticles = new List<Article>();
      List<Article> articles;
      if (showAll == false)
      {
        articles =  _articleDao.GetAll(x => x.LanguageCode == GetLanguage());
      }
      else
      {
        articles = _articleDao.GetAll();
      }
       
      if (filter != null)
      {
        if (filter.Subcategories != null && filter.Subcategories.Length > 0)
        {

          foreach (var subcategory in filter.Subcategories)
          {
            var selectedArticles = articles.Where(x => x.SubcategoryId == subcategory).ToList();
            foreach (var selectedArticle in selectedArticles)
            {
              tempArticles.Add(selectedArticle);
            }

          }
        }
        else
        {
          tempArticles = articles;
        }
        if (filter.Title != null)
        {
          tempArticles = tempArticles.Where(x => x.ArticleTitle.ToLower().Contains(filter.Title.ToLower())).ToList();
        }
      }
      else
      {
        tempArticles = articles;
      }


      return new SuccessDataResult<List<Article>>(tempArticles);
    }

    public IDataResult<ArticleDataModel> GetArticle(int articleId)
    {
      var article = _articleDao.Get(x => x.ArticleId == articleId);
      var articleContent = _fileOperationsService.ReadFile(mainPath, article.ArticleFilePath);
      var articleData = _mapper.Map<ArticleDataModel>(article);
      articleData.ArticleContent = articleContent.Data;
      return new SuccessDataResult<ArticleDataModel>(articleData);
    }


    public IDataResult<List<Article>> GetArticlesBySubcategoryId(int subCategoryId)
    {
      return new SuccessDataResult<List<Article>>(_articleDao.GetAll(x => x.SubcategoryId == subCategoryId && x.LanguageCode == GetLanguage()));
    }


    //Daha sonra eklenecek...
    public IDataResult<List<Article>> GetArticlesWithArticleTitle(string title)
    {
      throw new NotImplementedException();
    }
  }
}

