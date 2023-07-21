using System;
using System.Security.Claims;
using Abstract;
using Business.Abstract;
using Entities.Concrete.DTOs.Responses;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class QuestionController : ControllerBase
  {
    IQuestionService _questionService;
    IQuestionGroupService _questionGroupService;
    IAnswerService _answerService;
    IUserQuizService _userQuizService;

    public QuestionController(IUserQuizService userQuizService,IQuestionService questionService, IQuestionGroupService questionGroupService, IAnswerService answerService)
    {
      _questionService = questionService;
      _answerService = answerService;
      _questionGroupService = questionGroupService;
      _userQuizService = userQuizService;
    }

    [Authorize]
    [HttpPost("AddQuestion")]
    public IActionResult AddQuestion(AddQuestionDto question)
    {
      var result = _questionService.AddQuestionWithAddedUser(question);
      return result.Success ? Ok(result) : BadRequest(result);
    }

    [Authorize]
    [HttpPost("DeleteQuestion")]
    public IActionResult DeleteQuestion(int questionId)
    {
      var question = _questionService.GetByQuestionId(questionId);
      var result = _questionService.Delete(question.Data);
      return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("GetByQuestionId")]
    public IActionResult GetQuestionById(int questionId)
    {
      var result = _questionService.GetByQuestionId(questionId);
      return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("GetQuestions")]
    public IActionResult GetQuestions()
    {
      var result = _questionService.GetAll();
      return result.Success ? Ok(result) : BadRequest(result);
    }
    [HttpGet("GetQuestionGroupsByArticleId")]
    public IActionResult GetQuestionGroups(int articleId)
    {
      var result =_questionGroupService.GetAllByArticleId(articleId);
      return Ok(result);
    }
    [HttpGet("GetQuestionsByQuestionGroupId")]
    public IActionResult GetQuestionsByQuestionGroupId(int questionGroupId)
    {
      var result = _questionService.GetQuestionsByQuestionGroupId(questionGroupId);
      return Ok(result);
    }
    //şimdilik deneme amaçlı buraya ekliyorum sonra bunu questiongroupcontrollere taşırız.
    [HttpPost("addquestiongroup")]
    public IActionResult AddQuestionGroup(QuestionGroup questionGroup)
    {
      var result = _questionGroupService.Add(questionGroup);
      return Ok(result);
    }
    [HttpGet("GetQuestionGroups")]
    public IActionResult GetQuestionGroups()
    {
      var result = _questionGroupService.GetAll();
      return Ok(result);
    }
    [HttpGet("GetQuizAnswers")]
    public IActionResult GetQuizAnswers(int questionGroupId){
      var questions = _questionService.GetQuestionsWithAnswersByQuestionGroupId(questionGroupId);
      if(questions.Success){
        return Ok(questions);
      }
      return BadRequest(questions);
    }
    [HttpGet("endquiz")]
    public IActionResult EndQuiz(int questionGroupId){
      var userId = HttpContext.User.Claims.First(x=>x.Type == ClaimTypes.NameIdentifier).Value;
      var result = _userQuizService.EndQuiz(userId,questionGroupId);
      return Ok(result);
    }
  }
}

