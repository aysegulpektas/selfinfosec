using System;
using Core.Utilities.Results;
using Entities.Concrete.DTOs;
using Entities.Concrete.DTOs.Responses;
using Entities.Models;

namespace Business.Abstract
{
	public interface IArticleService
	{
        IResult Add(AddArticleDto article);
        IResult Delete(int articleId);
        IDataResult<ArticleDataModel> GetArticle(int articleId);
        IDataResult<List<Article>> GetArticlesBySubcategoryId(int subCategoryId);
        IDataResult<List<Article>>GetArticlesWithArticleTitle(string title);
        IDataResult<List<Article>> GetAllArticles(ArticleFilterDto filter=null, bool showAll = false);
    }
}


