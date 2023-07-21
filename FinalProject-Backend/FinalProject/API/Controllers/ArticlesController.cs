using Business.Abstract;
using Entities.Concrete.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        IArticleService _articleService;
        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        [Authorize(Roles="ADMIN")]
        [HttpPost("AddArticle")]
        public IActionResult AddArticle(AddArticleDto article)
        {
            var result = _articleService.Add(article);
            return Ok(result);
        }


        [Authorize(Roles="ADMIN")]
        [HttpDelete("DeleteArticle")]
        public IActionResult DeleteArticle(int articleId)
        {
            var result = _articleService.Delete(articleId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetArticle")]
        public IActionResult GetArticle(int articleId)
        {
            var result = _articleService.GetArticle(articleId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        /*İçeriği HTML sayfası olarak yönlendireceğiz */
        [Consumes("text/html")]
        [HttpGet("Test")]
        public ContentResult ReturnHTML()
        {
            return base.Content("<b>Merhaba</b>", "text/html");
        }
        [HttpGet("GetArticlesBySubcategoryId")]
        public IActionResult GetArticlesBySubcategoryId(int subcategoryId)
        {
            var response = _articleService.GetArticlesBySubcategoryId(subcategoryId);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpGet("getallarticles")]
        public IActionResult GetAllArticles(string? subcategories=null,string? title=null)
        {
            ArticleFilterDto filter = null;
            if(subcategories != null || title != null){
               filter = new ArticleFilterDto();
               if(title != null) {
                filter.Title = title;
               }
               if(subcategories != null){
                var numberString = subcategories.Split(',');
                List<int> numbers = new List<int>();
                foreach(string number in numberString){
                    int numberResult;
                    var tryResult = int.TryParse(number,out numberResult);
                    if(tryResult){
                        numbers.Add(numberResult);
                    }
                }
                filter.Subcategories = numbers.ToArray();
               }
            }
            var articles = _articleService.GetAllArticles(filter);
            return Ok(articles);

        }
    }
}
