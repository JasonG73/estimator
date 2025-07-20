using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Estimator.Data.Tenant;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Estimator.Components.Shared;

public class BaseTenantAreaService(TenantAreaDbContext dbContext,
    AuthenticationStateProvider authenticationStateProvider, ILogger logger
    )
{

    public record TenantInfo(string UserId, string TenantId);
    protected readonly TenantAreaDbContext _dBContext = dbContext;

    protected readonly ILogger _logger = logger;

    public async Task<TenantInfo?> GetTenantInfo()
    {

        var auth = await authenticationStateProvider.GetAuthenticationStateAsync();

        var tenantId = auth.User.Claims.Where(c => c.Type == "TenantId").Select(c => c.Value).SingleOrDefault();
        var userId = auth.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).Single();

        if (string.IsNullOrEmpty(tenantId))
        {
            _logger.LogError($"Tenant Id not found for user:{userId}");
            return null;
        }


        return new TenantInfo(userId, tenantId);
    }   

}
