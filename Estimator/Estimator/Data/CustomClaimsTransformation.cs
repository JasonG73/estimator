using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Estimator.Data;

public class CustomClaimsTransformation(IServiceProvider serviceProvider) : IClaimsTransformation
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return principal;
        }



        using (var scope = _serviceProvider.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var identity = new ClaimsIdentity();

                // Example: Add a custom claim
                if (!principal.HasClaim(c => c.Type == "TenantId"))
                {
                    identity.AddClaim(new Claim("TenantId", user.TenantId));
                }

                if (identity.Claims.Any())
                {
                    principal.AddIdentity(identity);
                }
            }
        }

        return principal;
    }
}
