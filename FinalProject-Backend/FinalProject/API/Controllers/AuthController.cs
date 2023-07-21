using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    IAuthService _authService;
    public AuthController(IAuthService authService)
    {
      _authService = authService;
    }
    [ProducesResponseType(typeof(Result),200)]
    [HttpPost("register")]
    public IActionResult Register(UserRegisterModel registerModel)
    {
      var result = _authService.Register(registerModel);
      return result.Success ? Ok(result) : BadRequest(result);
    }
    
    [ProducesResponseType(typeof(DataResult<ResponseTokenModel>),200)]
    [HttpPost("login")]
    public IActionResult Login(UserLoginModel loginModel)
    {
      var result = _authService.Login(loginModel);
      return result.Success ? Ok(result) : BadRequest(result);
    }
  }
}
