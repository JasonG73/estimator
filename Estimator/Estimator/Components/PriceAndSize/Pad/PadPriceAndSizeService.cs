using System;
using Estimator.Components.Shared;
using Estimator.Data.Entities;
using Estimator.Data.Tenant;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Estimator.Components.PriceAndSize.Pad;

public class PadPriceAndSizeService(TenantAreaDbContext dbContext, AuthenticationStateProvider authenticationStateProvider, ILogger<PadPriceAndSizeService> logger)
    : BaseTenantAreaService(dbContext, authenticationStateProvider, logger)
{

    public async Task<List<PadPriceAndSize>> GetPadPriceAndSizes()
    {

        TenantInfo? tenantInfo = await GetTenantInfo();

        if (tenantInfo is null)
        {
            return null;
        }

        return await _dBContext.PadPriceAndSizes
        .Where(p => p.TenantId == tenantInfo.TenantId)
        .ToListAsync();

    }

    public async Task<PadPriceAndSize?> GetPadPriceAndSize(int id)
    {        
        
        TenantInfo? tenantInfo = await GetTenantInfo();

        if (tenantInfo is null)
        {
            return null;
        }

        var entity = await _dBContext.PadPriceAndSizes.Where(f => f.Id == id && f.TenantId == tenantInfo.TenantId)
        .FirstOrDefaultAsync();

        return entity;

    }

    public async void RemovePadPriceAndSize(int id)
    {
        
        var entity = await GetPadPriceAndSize(id);
        if (entity is not null)
        {
            _dBContext.PadPriceAndSizes.Remove(entity);
            await _dBContext.SaveChangesAsync();
        }
    }

    public async Task<PadPriceAndSize> SavePadPriceAndSize(PadPriceAndSize newPadPriceAndSize)
    {


        TenantInfo? tenantInfo = await GetTenantInfo();

        if (tenantInfo is null)
        {
            return null;
        }

        newPadPriceAndSize.TenantId = tenantInfo.TenantId;

        _dBContext.PadPriceAndSizes.Add(newPadPriceAndSize);
        await _dBContext.SaveChangesAsync();

        return newPadPriceAndSize;


    }

    public async Task<PadPriceAndSize?> UpdatePadPriceAndSize(int padId, PadPriceAndSize updatedPadPriceAndSize)
    {
        TenantInfo? tenantInfo = await GetTenantInfo();

        if (tenantInfo is null)
        {
            return null;
        }


        var entity = await _dBContext.PadPriceAndSizes.Where(f => f.Id == padId && f.TenantId == tenantInfo.TenantId)
        .FirstOrDefaultAsync();

        if (entity is not null)
        {
            entity.Code = updatedPadPriceAndSize.Code;
            entity.Length = updatedPadPriceAndSize.Length;
            entity.Height = updatedPadPriceAndSize.Height;
            entity.LabourPerEach = updatedPadPriceAndSize.LabourPerEach;
            entity.Width = updatedPadPriceAndSize.Width;
            await _dBContext.SaveChangesAsync();


        }

        return entity;

    }

}
