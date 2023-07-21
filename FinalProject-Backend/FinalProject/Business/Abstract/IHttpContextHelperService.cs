using System;
namespace Business.Abstract
{
	public interface IHttpContextHelperService
	{
        string GetUserId();
        IDictionary<string,string> GetHeaders();

    }
}

