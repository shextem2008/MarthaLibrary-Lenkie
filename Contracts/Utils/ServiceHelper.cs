﻿using Contracts.Extensions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Contracts.Utils
{
    public interface IServiceHelper
    {
        //Task<LMEGenericException> GetExceptionAsync(string errorCode);
        T GetOrUpdateCacheItem<T>(string key, Func<T> update, int? cacheSeconds = null);
        string GetCurrentUserEmail();
        int? GetCurrentUserId();
        int GetTenantId();
        //string GetCurrentUserTerminal();
        Uri GetAbsoluteUri();
    }
    public class ServiceHelper : IServiceHelper
    {
        //readonly IErrorCodeService _errorCodesSvc;
        readonly ICacheManager _cacheManager;
        readonly IHttpContextAccessor _httpContext;
        public ServiceHelper( ICacheManager cacheManager, IHttpContextAccessor httpContext)
        {
          
            _cacheManager = cacheManager;
            _httpContext = httpContext;
        }

        public string GetCurrentUserEmail()
        {
            //var emailb = _httpContext.HttpContext?.User?.FindFirst("Name")?.Value;
            var email = _httpContext.HttpContext?.User?.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
            //string tenantid = _httpContext.HttpContext?.User?.FindFirst("preferred_username")?.Value;
            return !string.IsNullOrEmpty(email) ? email : "Anonymous";
        }

        //public string GetCurrentUserTerminal()
        //{
        //    var terminal = _httpContext.HttpContext?.User?.FindFirst("location")?.Value;
        //    return !string.IsNullOrEmpty(terminal) ? terminal : "";
        //}
        public Uri GetAbsoluteUri()
        {
            var request = _httpContext.HttpContext.Request;

            var uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Path = request.Path.ToString(),
                Query = request.QueryString.ToString()
            };

            return uriBuilder.Uri;
        }

        public int? GetCurrentUserId()
        {
            var id = _httpContext.HttpContext?.User?.FindFirst("id")?.Value;
            return id is null ? (int?)null : int.Parse(id);
        }
        public int GetTenantId()
        {
            var id = _httpContext.HttpContext?.User?.FindFirst("preferred_username")?.Value;
            return id is null ? 0 : int.Parse(id);
        }

        //public async Task<LMEGenericException> GetExceptionAsync(string errorCode)
        //{
        //    var error = await GetOrUpdateCacheItem(errorCode, async () => await _errorCodesSvc.GetErrorByCodeAsync(errorCode));

        //    if (error is null)
        //        throw new LMEGenericException("Error validating your request. Please try again.", errorCode);

        //    return new LMEGenericException(error.Message, error.Code);
        //}

        public T GetOrUpdateCacheItem<T>(string key, Func<T> update, int? cacheSeconds = null)
        {
            var item = cacheSeconds is null ? _cacheManager.Get(key, update) : _cacheManager.Get(key, cacheSeconds.Value, update);
            return (T)item;
        }
    }
}
