using System;
using Business.Abstract;
using Core.Utilities.Results;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
	{
        IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
		{
            _userProfileService = userProfileService;
		}
        [HttpGet("GetUserProfile")]
        public IActionResult GetUserProfile(string userId)
        {
            var result = _userProfileService.GetUserProfile(userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetMyProfile")]
        public IActionResult GetMyProfile()
        {
            var result = _userProfileService.GetMyProfile();
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}

