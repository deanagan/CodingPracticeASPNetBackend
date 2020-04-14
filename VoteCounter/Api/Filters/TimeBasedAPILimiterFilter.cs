using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Api.Interfaces;
using Api.Controllers;

namespace Api.Filters
{
    public class TimeBasedAPILimiterFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var voteCounterController = ((VoteCounterController)context.Controller);

            var id = Convert.ToInt32(context.ActionArguments["id"]);
            if (!voteCounterController.RateLimiter.RateLimit(id))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.Result = new ContentResult { Content = "Too Many Requests" };
            }

            base.OnActionExecuting(context);
        }


    }
}
