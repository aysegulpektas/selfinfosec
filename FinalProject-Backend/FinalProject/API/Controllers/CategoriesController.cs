
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [Authorize]
        [HttpPost("AddCategory")]
        public IActionResult AddCategory(Category category){
            var result = _categoryService.Add(category);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [Authorize(Roles="ADMIN")]
        [HttpPost("DeleteCategory")]
        public IActionResult DeleteCategory(int categoryId){
            var category = _categoryService.GetById(categoryId);
            var result = _categoryService.Delete(category.Data);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetByCategoryId")]
        public IActionResult GetCategoryById(int categoryId){
            var result = _categoryService.GetById(categoryId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetCategories")]
        public IActionResult GetCategories(){
            var result = _categoryService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}

