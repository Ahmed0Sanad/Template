using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Core.Service.Contract;

namespace Template.Helper
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int timeToCache;

        public CacheAttribute(int TimeToCache)
        {

            timeToCache = TimeToCache;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _cacheService = context.HttpContext.RequestServices.GetService(typeof(ICacheService)) as ICacheService;
            string key = GenerateCacheKey(context.HttpContext.Request);
            string? result = await _cacheService.GetCachedData(key);
            if (!string.IsNullOrEmpty(result))
            {
                context.Result = new ContentResult
                {
                    Content = result,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                return;
            }
            var executedContext = await next.Invoke();
            if (executedContext.Result is ObjectResult objectResult && objectResult.Value is not null)
            {
                var cacheData = objectResult.Value;

                await _cacheService.SetCachedData(key, cacheData, TimeSpan.FromMinutes(timeToCache));

            }

        }

        private string GenerateCacheKey(HttpRequest request)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(request.Path);
            foreach (var item in request.Query.OrderBy(x => x.Key))
            {
                sb.Append($"|{item.Key}={item.Value}");
            }
            return sb.ToString();
        }
    }
}
