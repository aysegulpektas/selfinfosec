using Abstract;
using AutoMapper;
using Business.Abstract;
using Business.Tools;
using Entities.Concrete.DTOs;
using Entities.Concrete.DTOs.Responses;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    IUserService _userService;
    IMapper _mapper;
    IHttpContextHelperService _httpContextHelperService;
    IResetCodeService _resetCodeService;
    public UserController(IUserService userService, IMapper mapper, IHttpContextHelperService httpContextHelperService,IResetCodeService resetCodeService)
    {
      _userService = userService;
      _mapper = mapper;
      _httpContextHelperService = httpContextHelperService;
      _resetCodeService = resetCodeService;
    }
    [HttpGet("GetUsers")]
    public IActionResult GetUsers()
    {
      List<UserResponseModel> userResponses = new List<UserResponseModel>();
      var users = _userService.GetAll();
      foreach (var user in users.Data)
      {
        var response = _mapper.Map<UserResponseModel>(user);
        userResponses.Add(response);
      }
      return Ok(userResponses);

    }
    [HttpGet("GetMe")]
    public IActionResult GetMe()
    {
      var userId = _httpContextHelperService.GetUserId();
      var currentuser = _userService.GetById(userId);
      var response = _mapper.Map<UserResponseModel>(currentuser.Data);
      return Ok(response);
    }


    [HttpPost("SendResetCode")]
    public IActionResult SendRestCode(SendResetCodeDto resetCodeDto){
      
      var result = _resetCodeService.SendResetCode(resetCodeDto);
      if(result.Success){
        return Ok("Şifre sıfırlama maili gönderildi");
      }else{
        return BadRequest("Bir hata oluştu");
      }
    }
    [HttpPost("ResetPasswordWithCode")]
    public IActionResult ResetCodeWithCode(ResetPasswordWithCodeDto dto){
      var result = _resetCodeService.ResetCode(dto);
      if(result.Success){
        return Ok("Şifre sıfırlandı");
      }else{
        return BadRequest("Bir hata oluştu");
      }
    }
    [Authorize]
    [HttpPost("UpdatePasswordUser")]
    public IActionResult UpdatePasswordUser(PasswordResetDto passwordResetDto)
    {
      var result = _userService.UpdatePassword(passwordResetDto);
      return result.Success ? Ok(result) : BadRequest(result);
    }
    [HttpGet("TestEndpoint")]
    public IActionResult TestEndpoint()
    {
      //Test
      return Ok("Working");
    }
    [Authorize]
    [HttpPost("UpdateProfile")]
    public IActionResult UpdateProfile(EditProfileDto editProfile)
    {
      var result = _userService.UpdateUser(editProfile);
      return result.Success ? Ok(result) : BadRequest(result);
    }

  }
}
