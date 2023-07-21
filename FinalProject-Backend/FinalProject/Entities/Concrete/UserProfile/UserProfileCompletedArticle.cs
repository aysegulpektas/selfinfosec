using System;
namespace Entities.Concrete.UserProfile
{
	public class UserProfileCompletedArticle
	{
		public UserProfileCompletedArticle()
		{
        }

	public int ArticleId { get; set; }
    public string ArticleTitle { get; set; }
    public double SuccessRate { get; set; } //0 - 1 arasında bir sayı

    }
}

