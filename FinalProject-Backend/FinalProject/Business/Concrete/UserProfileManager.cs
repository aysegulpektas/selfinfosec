using System;
using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete.UserProfile;
using Responses;

namespace Business.Concrete
{
	public class UserProfileManager : IUserProfileService
	{
        IHttpContextHelperService _httpHelperService;
        IUserAnswersService _userAnswersService;
        IUserService _userService;


        public UserProfileManager(IHttpContextHelperService httpHelperService, IUserService userService,IUserAnswersService userAnswersService)
		{
            _userAnswersService = userAnswersService;
            _httpHelperService = httpHelperService;
            _userService = userService;

		}

        public IDataResult<UserProfileModel> GetMyProfile()
        {
            var userId = _httpHelperService.GetUserId();
            var user = _userService.GetById(userId).Data;
            var userInfo = new UserProfileInfoModel();
            userInfo.UserName = user.UserName;
            userInfo.UserId = user.UserId;
            userInfo.FirstName = user.FirstName;
            userInfo.LastName = user.Lastname;
            userInfo.Email = user.Email;
            var completedArticles = _userAnswersService.GetSuccessRate(user.UserId).Data;
            var userProfileModel = new UserProfileModel();
            userProfileModel.UserInfo = userInfo;
            userProfileModel.CompletedArticles = completedArticles;
            return new SuccessDataResult<UserProfileModel>(userProfileModel);
        }

        public IDataResult<UserProfileModel> GetUserProfile(string userId)
        {
            var user = _userService.GetById(userId).Data;
            var userInfo = new UserProfileInfoModel();
            userInfo.UserName = user.UserName;
            userInfo.UserId = user.UserId;
            userInfo.FirstName = user.FirstName;
            userInfo.LastName = user.Lastname;
            userInfo.Email = user.Email;
            var completedArticles = _userAnswersService.GetSuccessRate(user.UserId).Data;
            var userProfileModel = new UserProfileModel();
            userProfileModel.UserInfo = userInfo;
            userProfileModel.CompletedArticles = completedArticles;
            return new SuccessDataResult<UserProfileModel>(userProfileModel);

        }
    }
}


