using System;
using Estimator.Components.Shared;
using Estimator.Data.Tenant;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Estimator.Components.Company;

public class CompanyService(TenantAreaDbContext dbContext, AuthenticationStateProvider authenticationStateProvider, ILogger<CompanyService> logger)
    : BaseTenantAreaService(dbContext, authenticationStateProvider, logger)
{

    public async Task<List<Data.Entities.Company>?> GetCompanies()
    {
        TenantInfo? tenantInfo = await GetTenantInfo();

        if (tenantInfo is null)
        {
            return null;
        }

        return await _dBContext.Companies
            .Where(c => c.TenantId == tenantInfo.TenantId)
            .ToListAsync();
    }

    

    


}
