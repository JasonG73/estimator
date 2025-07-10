using System;
using Estimator.Components.Shared;
using Estimator.Data.Entities;
using Estimator.Data.Tenant;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Estimator.Components.PriceAndSize.Pad;

public class PadPriceAndSizeService(TenantAreaDbContext dbContext, AuthenticationStateProvider authenticationStateProvider)
    : BaseTenantAreaService(dbContext, authenticationStateProvider)
{

    public async Task<List<PadPriceAndSize>> GetPadPriceAndSizes()
    {

        var tenantId = await GetTenantId();

        if (string.IsNullOrEmpty(tenantId))
        {
            throw new Exception("error with tenantId");
        }
        return await _dBContext.PadPriceAndSizes
        .Where(p => p.TenantId == tenantId)
        .ToListAsync();

    }

    public async Task<PadPriceAndSize?> GetPadPriceAndSize(int id)
    {        
        
        var tenantId = await GetTenantId();

        if (tenantId == null)
        {
            return null;
        }

        var entity = await _dBContext.PadPriceAndSizes.Where(f => f.Id == id && f.TenantId == tenantId)
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


        var tenantId = await GetTenantId();

        if (string.IsNullOrEmpty(tenantId))
        {
            throw new Exception("error with tenantId");
        }

        newPadPriceAndSize.TenantId = tenantId;

        _dBContext.PadPriceAndSizes.Add(newPadPriceAndSize);
        await _dBContext.SaveChangesAsync();

        return newPadPriceAndSize;


    }

    public async Task<PadPriceAndSize?> UpdatePadPriceAndSize(int padId, PadPriceAndSize updatedPadPriceAndSize)
    {
        var tenantId = await GetTenantId();

        if (string.IsNullOrEmpty(tenantId))
        {
            throw new Exception("error with tenantId");
        }


        var entity = await _dBContext.PadPriceAndSizes.Where(f => f.Id == padId && f.TenantId == tenantId)
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
