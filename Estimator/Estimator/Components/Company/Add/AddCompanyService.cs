using System;
using Estimator.Components.Shared;
using Estimator.Data.Tenant;
using Microsoft.AspNetCore.Components.Authorization;

namespace Estimator.Components.Company.Add;

public class AddCompanyService(TenantAreaDbContext dbContext, AuthenticationStateProvider authenticationStateProvider, ILogger<AddCompanyService> logger)
    : BaseTenantAreaService(dbContext, authenticationStateProvider, logger)
{
    public async Task<Data.Entities.Company?> AddCompanyAsync(Data.Entities.Company newCompany)
    {
        TenantInfo? tenantInfo = await GetTenantInfo();

        if (tenantInfo is null)
        {
            return null;
        }

        newCompany.TenantId = tenantInfo.TenantId;

        _dBContext.Companies.Add(newCompany);

        await _dBContext.SaveChangesAsync();

        return newCompany;
    }

}
