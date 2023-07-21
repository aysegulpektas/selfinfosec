using Business.Abstract;
using Business.Tools;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using Entities.Concrete.DTOs;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
  public class AuthManager : IAuthService
  {
    IUserService _userService;
    IRoleService _roleService;
    IConfiguration _configuration;
    public AuthManager(IUserService userService,IRoleService roleService,IConfiguration configuration)
    {
      _userService = userService;
      _roleService = roleService;
      _configuration = configuration;
    }
    public IDataResult<ResponseTokenModel> Login(UserLoginModel loginModel)
    {
      var requestedUser = _userService.GetByUsername(loginModel.Username);
      if(requestedUser is null)
      {
        return new ErrorDataResult<ResponseTokenModel>(ConstantsFolder.Constants.Auth_Login_Error);
      }
      var passwordVerify = HashingHelper.VerifyPasswordHash(loginModel.Password, requestedUser.Data.PasswordHash, requestedUser.Data.PasswordSalt);
      if (!passwordVerify)
      {
        return new ErrorDataResult<ResponseTokenModel>(ConstantsFolder.Constants.Auth_Login_WrongPassword);
      }
      var exp = DateTime.Now.AddHours(2);
      var signingKey = _configuration.GetSection("JWTSecurity:SigningKey").Value;
      var issuer = _configuration.GetSection("JWTSecurity:Issuer").Value;
      var roleId = requestedUser.Data.Roleld < 0 ? 1 : requestedUser.Data.Roleld;
      var roleName = _roleService.GetByRoleId(roleId).Data.RoleName; 
      var jwtCreator = new JWTCreator(signingKey, issuer, requestedUser.Data.UserId, roleName, exp, null);
      var token = jwtCreator.GenerateToken();
      var responseToken = new ResponseTokenModel();
      responseToken.Token = token;
      responseToken.Expiration = exp;
      responseToken.Username = loginModel.Username;
      responseToken.UserRole = roleName;
      return new SuccessDataResult<ResponseTokenModel>(ConstantsFolder.Constants.Auth_Login,responseToken);
    }

    public IResult Register(UserRegisterModel registerModel)
    {
      byte[] hash;
      byte[] salt;
      HashingHelper.CreatePasswordHash(registerModel.Password, out hash, out salt);
      var newUser = new User(registerModel.Username, registerModel.FirstName, registerModel.LastName, registerModel.Email, hash, salt, 1);
      var userAddResult = _userService.AddUser(newUser);
      IResult result;
      if (userAddResult.Success)
      {
        result = new SuccessResult(ConstantsFolder.Constants.Auth_Register);
      }
      else
      {
        result = new ErrorResult(ConstantsFolder.Constants.Auth_Register_Error);
      }
      return result;
    }
  }
}
