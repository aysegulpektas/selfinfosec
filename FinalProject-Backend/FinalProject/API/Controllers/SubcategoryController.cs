using System;
using Business.Abstract;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoryController : ControllerBase
	{
		ISubcategoryService _subcategoryService;

		public SubcategoryController(ISubcategoryService subcategoryService)
		{
			_subcategoryService = subcategoryService;
		}

        [Authorize]
        [HttpPost("AddSubcategory")]
        public IActionResult AddSubcategory(Subcategory subcategory)
        {
            var result = _subcategoryService.Add(subcategory);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpPost("DeleteSubcategory")]
        public IActionResult DeleteSubcategory(int subcategoryId)
        {
            var subcategory = _subcategoryService.GetById(subcategoryId);
            var result = _subcategoryService.Delete(subcategory.Data);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetSubcategories")]
        public IActionResult GetSubcategories()
        {
            var result = _subcategoryService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetBySubcategoryId")]
        public IActionResult GetSubcategoryById(int subcategoryId)
        {
            var result = _subcategoryService.GetById(subcategoryId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetAllByCategoryId")]
        public IActionResult GetAllByCategoryId(int categoryid)
        {
            var result = _subcategoryService.GetAllByCategoryId(categoryid);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}

