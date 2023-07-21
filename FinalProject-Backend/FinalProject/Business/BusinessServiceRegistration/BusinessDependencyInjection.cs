using Abstract;
using Business.Abstract;
using Business.Concrete;
using Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessServiceRegistration
{
  public static class BusinessDependencyInjection
  {
    public static IServiceCollection BusinessServiceRegister(this IServiceCollection services)
    {
      services.AddScoped<IAnswerService, AnswerManager>();
      services.AddScoped<IArticleService, ArticleManager>();
      services.AddScoped<ICategoryService, CategoryManager>();
      services.AddScoped<IQuestionService, QuestionManager>();
      services.AddScoped<IRoleService, RoleManager>();
      services.AddScoped<ISubcategoryService, SubcategoryManager>();
      services.AddScoped<IUserAnswersService, UserAnswersManager>();
      services.AddScoped<IUserService, UserManager>();
      services.AddScoped<IAuthService, AuthManager>();
      services.AddScoped<IHttpContextHelperService, HttpContextHelperManager>();
      services.AddScoped<IFileOperationsService, FileOperationsManager>();
      services.AddScoped<IQuestionGroupService, QuestionGroupManager>();
      services.AddScoped<IUserProfileService, UserProfileManager>();
      services.AddScoped<ISequencedImageService, SequencedImageManager>();
      services.AddScoped<IUserQuizService,UserQuizManager>();
      services.AddScoped<IResetCodeService,ResetCodeManager>();
      return services;

    }
  }
}
