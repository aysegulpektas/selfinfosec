using System;
using Core.Utilities.Results;
using Entities.Concrete.UserProfile;

namespace Business.Abstract
{
    public interface IUserProfileService
    {
        IDataResult<UserProfileModel> GetUserProfile(string userId);
        IDataResult<UserProfileModel> GetMyProfile();
    }
}

