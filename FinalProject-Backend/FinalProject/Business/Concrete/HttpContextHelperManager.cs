using System;
using System.Security.Claims;
using Business.Abstract;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class HttpContextHelperManager : IHttpContextHelperService
    {
        IHttpContextAccessor _httpContextAccessor;
        public HttpContextHelperManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IDictionary<string, string> GetHeaders()
        {
            IDictionary<string, string> headerList = new Dictionary<string, string>();
            var headers = _httpContextAccessor.HttpContext.Request.Headers;
            foreach (var header in headers)
            {

              headerList.Add(header.Key, header.Value);

            }
            return headerList;
        }

        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}

