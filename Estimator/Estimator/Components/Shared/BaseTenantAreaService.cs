using System;
using Estimator.Data.Tenant;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Estimator.Components.Shared;

public class BaseTenantAreaService(TenantAreaDbContext dbContext, AuthenticationStateProvider authenticationStateProvider)
{
    protected readonly TenantAreaDbContext _dBContext = dbContext;

    public async Task<string?> GetTenantId()
    {

        var auth = await authenticationStateProvider.GetAuthenticationStateAsync();

        var tenantId = auth.User.Claims.Where(c => c.Type == "TenantId").Select(c => c.Value).SingleOrDefault();

        return tenantId;
    }

}
