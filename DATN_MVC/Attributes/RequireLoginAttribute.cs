﻿using DATN_MVC.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DATN_MVC.Attributes
{
    public class RequireLoginAttribute : TypeFilterAttribute
    {
        public RequireLoginAttribute() : base(typeof(RequireLoginFilter))
        {
        }
    }

    public class RequireLoginFilter : IAuthorizationFilter
    {
        private readonly ISessionService _sessionService;

        public RequireLoginFilter(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!_sessionService.IsLoggedIn())
            {
                context.Result = new RedirectToActionResult("Login", "Auth",
                    new { returnUrl = context.HttpContext.Request.Path });
            }
        }
    }
}
