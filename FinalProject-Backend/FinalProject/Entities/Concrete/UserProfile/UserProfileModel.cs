using System;
using Responses;

namespace Entities.Concrete.UserProfile
{
	public class UserProfileModel
	{
		public UserProfileModel()
		{
		}
        public UserProfileInfoModel UserInfo { get; set; }
        public List<SuccessRateDto> CompletedArticles { get; set; }
    }
}

