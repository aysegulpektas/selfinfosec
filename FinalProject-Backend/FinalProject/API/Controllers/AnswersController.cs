using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AnswersController : ControllerBase
  {
    IUserAnswersService _userAnswersService;
    IAnswerService _answerService;
    public AnswersController(IUserAnswersService userAnswersService,IAnswerService answerService)
    {
      _userAnswersService = userAnswersService;
      _answerService = answerService;
    }
    [HttpGet("AnswerQuestion")]
    public IActionResult AnswerQuestion(int question,int answer)
    {
      var result = _userAnswersService.AnswerQuestion(question, answer);
      if (result.Success)
      {
        return Ok("Soruya verdiğiniz cevap kayıt edildi");
      }
      return BadRequest(result.Message ?? "Bir hata oluştu");
    }
    [HttpGet("GetAnswers")]
    public IActionResult GetAnswers()
    {
      var result = _answerService.GetAll();
      return Ok(result);
    }
    [HttpPost("AddAnswer")]
    public IActionResult AddAnswer(Answer answer)
    {
      var result =_answerService.Add(answer);
      if (result.Success)
      {
        return Ok(result);
      }
      return BadRequest(result);
    }

    [HttpGet("SuccessRate")]
    public IActionResult GetSuccessRate(string userId){
      var successRates = _userAnswersService.GetSuccessRate(userId);
      return Ok(successRates);
    }

  }
}

