using System;
using Estimator.Components.Shared;
using Estimator.Data.Entities;
using Estimator.Data.Tenant;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Estimator.Components.Company.Update;

public class UpdateCompanyService(ILogger<UpdateCompanyService> logger, TenantAreaDbContext dbContext, AuthenticationStateProvider authenticationStateProvider) 
        : BaseTenantAreaService(dbContext, authenticationStateProvider, logger)
    
{   
   
    public async Task<Data.Entities.Company?> UpdateCompanyAsync(int companyId, Data.Entities.Company updatedCompany)
    {
        TenantInfo? tenantInfo = await GetTenantInfo();

        if (tenantInfo is null)
        {
            return null;
        }

        Data.Entities.Company? company = await _dBContext.Companies
            .Where(c => c.Id == companyId && c.TenantId == tenantInfo.TenantId)
            .FirstOrDefaultAsync();

        if (company != null)
        {
            company.Address = updatedCompany.Address;
            company.Email = updatedCompany.Email;
            company.Mobile = updatedCompany.Mobile;
            company.Name = updatedCompany.Name;
            company.PhoneNumber = updatedCompany.PhoneNumber;

            await _dBContext.SaveChangesAsync();
            return company;
        }

        return null;
    }

    public async Task<Data.Entities.Company?> GetCompanyAsync(int companyId)
    {
        TenantInfo? tenantInfo = await GetTenantInfo();

        if (tenantInfo is null)
        {
            return null;
        }

        return await _dBContext.Companies
            .Where(c => c.Id == companyId && c.TenantId == tenantInfo.TenantId)
            .FirstOrDefaultAsync();
    }


}
