using System;
using Abstract;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Responses;

namespace Business.Concrete
{
  public class UserAnswersManager : IUserAnswersService
  {
    IUserAnswersDao _userAnswersDao;
    IQuestionService _questionService;
    IAnswerService _answerService;
    IQuestionGroupService _questionGroupService;
    IArticleService _articleService;
    IHttpContextHelperService _httpContextHelperService;
    IUserQuizService _userQuizService;
    public UserAnswersManager(IUserQuizService userQuizService, IArticleService articleService, IQuestionGroupService questionGroupService, IUserAnswersDao userAnswersDao, IHttpContextHelperService httpContextHelperService, IQuestionService questionService, IAnswerService answerService)
    {
      _userAnswersDao = userAnswersDao;
      _httpContextHelperService = httpContextHelperService;
      _answerService = answerService;
      _questionService = questionService;
      _questionGroupService = questionGroupService;
      _articleService = articleService;
      _userQuizService = userQuizService;


    }


    [Authorize(Roles = "ADMIN")]
    public IResult Add(UserAnswers userAnswers)
    {
      _userAnswersDao.Add(userAnswers);
      return new SuccessResult();
    }

    public IDataResult<bool> AnswerQuestion(int question, int answer)
    {
      var userId = _httpContextHelperService.GetUserId();
      var selectedQuestion = _questionService.GetByQuestionId(question);
      if (selectedQuestion.Data != null)
      {
        var currentAnswer = _userAnswersDao.GetAll(x => x.QuestionId == selectedQuestion.Data.QuestionId && x.UserId == userId);
        if (currentAnswer.Count() > 0)
        {
          return new ErrorDataResult<bool>("Soru daha önce cevaplandırılmış", false);
        }

      }
      if (selectedQuestion.Data == null)
      {
        return new ErrorDataResult<bool>("Soru bulunamadı", false);
      }
      var questionAnswers = _answerService.GetAnswersByQuestionId(selectedQuestion.Data.QuestionId);
      if (questionAnswers.Data != null && questionAnswers.Data.Where(x => x.AnswerId == answer).Count() > 0)
      {
        var userAnswer = new UserAnswers();
        userAnswer.UserId = userId;
        userAnswer.AnswersId = answer;
        userAnswer.QuestionId = question;
        _userAnswersDao.Add(userAnswer);
        return new SuccessDataResult<bool>(true);
      }
      return new ErrorDataResult<bool>(false);

    }

    [Authorize(Roles = "ADMIN")]
    public IResult Delete(UserAnswers userAnswers)
    {
      _userAnswersDao.Delete(userAnswers);
      return new SuccessResult();
    }


    public IDataResult<List<UserAnswers>> GetAll()
    {
      return new SuccessDataResult<List<UserAnswers>>(_userAnswersDao.GetAll());
    }


    public IDataResult<UserAnswers> GetById(int id)
    {
      return new SuccessDataResult<UserAnswers>(_userAnswersDao.Get(x => x.UserAnswersId == id));
    }


    public IDataResult<UserAnswers> GetByUserAndAnswerId(string userId, int answersId)
    {
      var answers = _userAnswersDao.Get(x => x.UserId == userId && x.AnswersId == answersId);
      return new SuccessDataResult<UserAnswers>(answers);
    }


    public IDataResult<List<UserAnswers>> GetByUserId(string userId)
    {
      return new SuccessDataResult<List<UserAnswers>>(_userAnswersDao.GetAll(x => x.UserId == userId));
    }


    public IDataResult<List<UserAnswers>> GetUserAnswersByAnswerId(int answerId)
    {
      return new SuccessDataResult<List<UserAnswers>>(_userAnswersDao.GetAll(x => x.AnswersId == answerId));
    }
    public IDataResult<List<SuccessRateDto>> GetSuccessRate(string userId)
    {


      List<SuccessRateDto> SuccessRates = new List<SuccessRateDto>();
      var articles = _articleService.GetAllArticles(null,true);
      var questionGroups = _questionGroupService.GetAll();
      var questions = _questionService.GetAll();
      var answers = _answerService.GetAll();
      var userAnswers = _userAnswersDao.GetAll(x => x.UserId == userId);

      bool isAnswered = false;
      foreach (var questionGroup in questionGroups.Data)
      {
        var isCompleted = _userQuizService.IsFinished(userId, questionGroup.QuestionGroupId);
        if(isCompleted.Data == false)
        {
          continue;
        }
        var article = articles.Data.First(x => x.ArticleId == questionGroup.ArticleId);
        var successRateItem = new SuccessRateDto();
        successRateItem.ArticleId = questionGroup.ArticleId;
        successRateItem.QuestionGroupId = questionGroup.QuestionGroupId;
        successRateItem.QuestionGroupText = questionGroup.GroupTitle;
        successRateItem.TrueAnswersCount = 0;
        successRateItem.WrongAnswersCount = 0;
        successRateItem.ArticleName = article.ArticleTitle;
        var questionList = questions.Data.Where(x => x.QuestionGroupId == questionGroup.QuestionGroupId);
        foreach (var question in questionList)
        {

          var answerList = _userAnswersDao.GetAll(x => x.QuestionId == question.QuestionId).Count;
          if (answerList > 0)
          {
            isAnswered = true;
          }
          var trueAnswer = answers.Data.Find(x => x.QuestionId == question.QuestionId && x.IsTrue == true);
          if (trueAnswer != null)
          {
            if (userAnswers.FindAll(x => x.AnswersId == trueAnswer.AnswerId).Count > 0)
            {
              successRateItem.TrueAnswersCount += 1;
            }
            else
            {
              successRateItem.WrongAnswersCount += 1;
            }
          }

        }
        if (isAnswered)
        {
          successRateItem.SuccessRate = Math.Round((double)successRateItem.TrueAnswersCount / (double)(successRateItem.WrongAnswersCount + successRateItem.TrueAnswersCount), 1);
          SuccessRates.Add(successRateItem);
        }

      }
      SuccessRates = SuccessRates.OrderBy(x => x.ArticleId).ToList();
      return new SuccessDataResult<List<SuccessRateDto>>(SuccessRates);
    }
  }
}

