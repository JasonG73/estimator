using System;
using Estimator.Components.Shared;
using Estimator.Data.Tenant;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Estimator.Components.Company;

public class CompanyService(TenantAreaDbContext dbContext, AuthenticationStateProvider authenticationStateProvider)
    : BaseTenantAreaService(dbContext, authenticationStateProvider)
{

    public async Task<List<Data.Entities.Company>> GetCompanies()
    {
        var tenantId = await GetTenantId();

        if (string.IsNullOrEmpty(tenantId))
        {
            throw new Exception("error with tenantId");
        }

        return await _dBContext.Companies
            .Where(c => c.TenantId == tenantId)
            .ToListAsync();
    }

    public async Task<Data.Entities.Company> AddCompanyAsync(Data.Entities.Company newCompany)
    {


        var tenantId = await GetTenantId();

        if (string.IsNullOrEmpty(tenantId))
        {
            throw new Exception("error with tenantId");
        }

        newCompany.TenantId = tenantId;

        _dBContext.Companies.Add(newCompany);

        await _dBContext.SaveChangesAsync();

        return newCompany;
    }

    public async Task<Data.Entities.Company?> UpdateCompanyAsync(int companyId, Data.Entities.Company updatedCompany)
    {
        var tenantId = await GetTenantId();

        if (string.IsNullOrEmpty(tenantId))
        {
            throw new Exception("error with tenantId");
        }

        Data.Entities.Company? company = await _dBContext.Companies
            .Where(c => c.Id == companyId && c.TenantId == tenantId)
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
        var tenantId = await GetTenantId();

        if (string.IsNullOrEmpty(tenantId))
        {
            throw new Exception("error with tenantId");
        }
        return await _dBContext.Companies
            .Where(c => c.Id == companyId && c.TenantId == tenantId)
            .FirstOrDefaultAsync();
    }



}
