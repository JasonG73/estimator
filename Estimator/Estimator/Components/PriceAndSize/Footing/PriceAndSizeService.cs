using Estimator.Components.Shared;
using Estimator.Data.Entities;
using Estimator.Data.Tenant;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Estimator.Components.PriceAndSize.Footing;

public class PriceAndSizeService(TenantAreaDbContext dbContext, AuthenticationStateProvider authenticationStateProvider)
    : BaseTenantAreaService(dbContext, authenticationStateProvider)
{

    public async Task<List<FootingPriceAndSize>> GetFootingPriceAndSizes()
    {
        var tenantId = await GetTenantId();

        if (string.IsNullOrEmpty(tenantId))
        {
            throw new Exception("error with tenantId");
        }

        return await _dBContext.FootingPriceAndSizes
        .Where(f => f.TenantId == tenantId)
        .ToListAsync();

    }

    public async Task<FootingPriceAndSize?> GetFootingPriceAndSize(int id)
    {

        var tenantId = await GetTenantId();

        if (tenantId == null)
        {
            return null;
        }

        var entity = await _dBContext.FootingPriceAndSizes.Where(f => f.Id == id && f.TenantId == tenantId)
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


        var tenantId = await GetTenantId();

        if (string.IsNullOrEmpty(tenantId))
        {
            throw new Exception("error with tenantId");
        }

        newFootingPriceAndSize.TenantId = tenantId;

        _dBContext.FootingPriceAndSizes.Add(newFootingPriceAndSize);
        await _dBContext.SaveChangesAsync();

        return newFootingPriceAndSize;


    }

    public async Task<FootingPriceAndSize?> UpdateFootingPriceAndSize(int footingId, FootingPriceAndSize updatedFootingPriceAndSzie)
    {
        var tenantId = await GetTenantId();

        if (string.IsNullOrEmpty(tenantId))
        {
            throw new Exception("error with tenantId");
        }


        var entity = await _dBContext.FootingPriceAndSizes.Where(f => f.Id == footingId && f.TenantId == tenantId)
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
