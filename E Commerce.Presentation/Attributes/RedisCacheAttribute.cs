using E_Commerce.Services_Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Attributes
{
    internal class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInMin;

        public RedisCacheAttribute(int DurationInMin = 5)
        {
            _durationInMin = DurationInMin; 
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheKey = CreateCacheKey(context.HttpContext.Request);

            var cacheValue = await cacheService.GetAsync(cacheKey);

            if (cacheValue is not null)
            {

                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK

                };


            }
            var ExecutedContext = await next.Invoke();
            if (ExecutedContext.Result is OkObjectResult result)
            {
                await cacheService.SetAsync( cacheKey,  result.Value!,  TimeSpan.FromMinutes(_durationInMin));
            }


        }

        // /api/Products
        // /api/Products?brandId=2&typeId=1
        // /api/Products?typeId=1
        // /api/Products?typeId=1&brandId=2
        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder Key = new StringBuilder();
            Key.Append(value: request.Path); // /api/Products
            foreach (var item in request.Query.OrderBy(keySelector: X => X.Key))
            {
                Key.Append(handler: $"|{item.Key}-{item.Value}"); // /api/Products|brandId-2|typeId-1
            }
            return Key.ToString();
        }
    }
}
