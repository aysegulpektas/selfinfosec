using System;
using Abstract;
using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete.DTOs;
using Entities.Concrete.DTOs.Responses;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using ShowQuestionAnswers;

namespace Business.Concrete
{
  public class QuestionManager : IQuestionService
  {
    IQuestionDao _questionDao;
    IHttpContextHelperService _httpHelperService;
    IAnswerService _answerService;
    IUserQuizService _userQuizService;
    IMapper _mapper;

    public QuestionManager(IUserQuizService userQuizService,IQuestionDao questionDao, IHttpContextHelperService httpContextHelperService, IMapper mapper, IAnswerService answerService)
    {
      _questionDao = questionDao;
      _answerService = answerService;
      _httpHelperService = httpContextHelperService;
      _userQuizService = userQuizService;
      _mapper = mapper;
    }

    [Authorize(Roles = "ADMIN")]
    public IDataResult<Question> Add(Question question)
    {
      var addedQuestion = _questionDao.Add(question);
      return new SuccessDataResult<Question>("Soru başarıyla eklendi",addedQuestion);
    }
    [Authorize(Roles = "ADMIN")]
    
    public IDataResult<Question> AddQuestionWithAddedUser(AddQuestionDto addQuestionDto)
    {
      Question question = _mapper.Map<Question>(addQuestionDto);
      question.AddedUser = _httpHelperService.GetUserId();
      question.AddedDate = DateTime.Now;
      var addedQuestion = _questionDao.Add(question);
      return new SuccessDataResult<Question>(addedQuestion);
    }
    [Authorize(Roles = "ADMIN")]
    public IResult Delete(Question question)
    {
      _questionDao.Delete(question);
      return new SuccessResult();
    }


    public IDataResult<List<Question>> GetAll()
    {
      return new SuccessDataResult<List<Question>>(_questionDao.GetAll());
    }

    public IDataResult<Question> GetByQuestionId(int id)
    {
      return new SuccessDataResult<Question>(_questionDao.Get(x => x.QuestionId == id));
    }

    public IDataResult<List<QuestionAnswerDto>> GetQuestionsByQuestionGroupId(int questionGroupId)
    {
      var userId = _httpHelperService.GetUserId();
      var accessQuestions = _userQuizService.JoinQuiz(userId,questionGroupId);
      if(accessQuestions.Success == false){
        return new ErrorDataResult<List<QuestionAnswerDto>>("Sınava erişiminiz engellendi",new List<QuestionAnswerDto>());
      }
      var questionsList = new List<QuestionAnswerDto>();
      var result = _questionDao.GetAll(x => x.QuestionGroupId == questionGroupId);
      foreach (var question in result)
      {
        var answers = _answerService.GetAnswersResponseByQuestionId(question.QuestionId).Data;
        QuestionAnswerDto questionDto = new QuestionAnswerDto(question.QuestionId, question.QuestionText, answers);
        questionsList.Add(questionDto);
      }
      return new SuccessDataResult<List<QuestionAnswerDto>>(questionsList);
    } 

    public IDataResult<List<QuestionWithAnswerDto>> GetQuestionsWithAnswersByQuestionGroupId(int questionGroupId)
    {
      var userId = _httpHelperService.GetUserId();
      var accessQuestions = _userQuizService.EndQuiz(userId,questionGroupId);
      if(accessQuestions.Success == false){
        return new ErrorDataResult<List<QuestionWithAnswerDto>>("Yanıtlara erişiminiz engellendi.",new List<QuestionWithAnswerDto>());
      }
      var questionsList = new List<QuestionWithAnswerDto>();
      var result = _questionDao.GetAll(x => x.QuestionGroupId == questionGroupId);
      foreach (var question in result)
      {
        var answerList = new List<AnswerWithTrue>();
        var answers = _answerService.GetAnswersByQuestionId(question.QuestionId).Data;
        foreach (var answer in answers)
        {
          answerList.Add(new AnswerWithTrue(answer.AnswerId,answer.AnswerText,answer.IsTrue));
        }
        QuestionWithAnswerDto questionDto = new QuestionWithAnswerDto(question.QuestionId, question.QuestionText, answerList);
        questionsList.Add(questionDto);
      }
      return new SuccessDataResult<List<QuestionWithAnswerDto>>(questionsList);
    } 
  }
}

