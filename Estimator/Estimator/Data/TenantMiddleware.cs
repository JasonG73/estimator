using System;
using System.Security.Claims;
using Estimator.Data.TenantExtention;
using Microsoft.AspNetCore.Identity;

namespace Estimator.Data;

public class TenantMiddleware
{

    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, TenantIdProvider tenantIdProvider,
        UserManager<ApplicationUser> userManager)
    {
        try
        {            
            if (httpContext.User.Identity!.IsAuthenticated)
            {               
                tenantIdProvider.TenantId = httpContext.User.FindFirstValue("tenantId");               
            }
            await _next(httpContext);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
