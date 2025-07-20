using Estimator.Components.Shared;
using Estimator.Data.Entities;
using Estimator.Data.Tenant;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Estimator.Components.PriceAndSize.Footing;

public class PriceAndSizeService(TenantAreaDbContext dbContext, AuthenticationStateProvider authenticationStateProvider, ILogger<PriceAndSizeService> logger)
    : BaseTenantAreaService(dbContext, authenticationStateProvider, logger)
{

    public async Task<List<FootingPriceAndSize>> GetFootingPriceAndSizes()
    {
        TenantInfo? tenantInfo = await GetTenantInfo();

        if (tenantInfo is null)
        {
            return null;
        }

        return await _dBContext.FootingPriceAndSizes
        .Where(f => f.TenantId == tenantInfo.TenantId)
        .ToListAsync();

    }

    public async Task<FootingPriceAndSize?> GetFootingPriceAndSize(int id)
    {

        TenantInfo? tenantInfo = await GetTenantInfo();

        if (tenantInfo is null)
        {
            return null;
        }

        var entity = await _dBContext.FootingPriceAndSizes.Where(f => f.Id == id && f.TenantId == tenantInfo.TenantId)
        .FirstOrDefaultAsync();

        return entity;

    }

    public async void RemoveFootingPriceAndSize(int id)
    {

        var entity = await GetFootingPriceAndSize(id);
        if (entity is not null)
        {
            _dBContext.FootingPriceAndSizes.Remove(entity);
            await _dBContext.SaveChangesAsync();
        }
    }

    public async Task<FootingPriceAndSize> SaveFootingPriceAndSize(FootingPriceAndSize newFootingPriceAndSize)
    {
        TenantInfo? tenantInfo = await GetTenantInfo();

        if (tenantInfo is null)
        {
            return null;
        }

        newFootingPriceAndSize.TenantId = tenantInfo.TenantId;

        _dBContext.FootingPriceAndSizes.Add(newFootingPriceAndSize);
        await _dBContext.SaveChangesAsync();

        return newFootingPriceAndSize;


    }

    public async Task<FootingPriceAndSize?> UpdateFootingPriceAndSize(int footingId, FootingPriceAndSize updatedFootingPriceAndSzie)
    {
        TenantInfo? tenantInfo = await GetTenantInfo();

        if (tenantInfo is null)
        {
            return null;
        }


        var entity = await _dBContext.FootingPriceAndSizes.Where(f => f.Id == footingId && f.TenantId == tenantInfo.TenantId)
        .FirstOrDefaultAsync();

        if (entity is not null)
        {
            entity.Code = updatedFootingPriceAndSzie.Code;
            entity.Height = updatedFootingPriceAndSzie.Height;
            entity.LabourCost = updatedFootingPriceAndSzie.LabourCost;
            entity.Width = updatedFootingPriceAndSzie.Width;
            await _dBContext.SaveChangesAsync();


        }

        return entity;

    }

     
    
    
}
