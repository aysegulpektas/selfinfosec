using Core.Utilities.Results;
using Entities.Concrete.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
  public interface IAuthService
  {
    public IResult Register(UserRegisterModel registerModel);
    public IDataResult<ResponseTokenModel> Login(UserLoginModel loginModel);
  }
}
