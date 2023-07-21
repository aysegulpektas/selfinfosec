using Abstract;
using Business.Abstract;
using DTOs;
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
  public class SequencedImagesController : ControllerBase
  {
    ISequencedImageService _sequencedImageService;
    public SequencedImagesController(ISequencedImageService sequencedImageService)
    {
      _sequencedImageService = sequencedImageService;
    }
    [Authorize]
    [HttpPost("upload")]
    public IActionResult UploadTestFile([FromForm] SequencedImageUploadDto sequencedImageDto)
    {
      var result = _sequencedImageService.Add(sequencedImageDto);
      if (result.Success)
      {
        return Ok(result);
      }
      else
      {
        return BadRequest(result);
      }
    }
    [HttpGet("getimages")]
    public IActionResult GetImagesByArticleId(int article)
    {
      var sequencedImages = _sequencedImageService.GetImagesByArticleId(article);
      return Ok(sequencedImages);
    }

  }
}
