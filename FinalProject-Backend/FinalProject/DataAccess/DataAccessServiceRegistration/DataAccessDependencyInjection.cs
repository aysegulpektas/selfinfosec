using Abstract;
using Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessServiceRegistration
{
  public static class DataAccessDependencyInjection
  {
    public static IServiceCollection DataAccessServiceRegister(this IServiceCollection services)
    {
      services.AddScoped<IAnswerDao, EFAnswerDao>();
      services.AddScoped<IArticleDao, EFArticleDao>();
      services.AddScoped<ICategoryDao, EFCategoryDao>();
      services.AddScoped<IQuestionDao, EFQuestionDao>();
      services.AddScoped<IRoleDao, EFRoleDao>();
      services.AddScoped<ISubcategoryDao, EFSubcategoryDao>();
      services.AddScoped<IUserAnswersDao, EFUserAnswersDao>();
      services.AddScoped<IUserDao, EFUserDao>();
      services.AddScoped<ISequencedImageDao,EFSequencedImageDao>();
      services.AddScoped<IQuestionGroupDao, EFQuestionGroupDao>();
      services.AddScoped<IUserQuizDao,EFUserQuizDao>();
      services.AddScoped<IResetCodeDao,EFResetCodeDao>();
      return services;
    }
  }
}
