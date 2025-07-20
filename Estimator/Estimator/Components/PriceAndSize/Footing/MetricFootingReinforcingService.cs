using System;
using Estimator.Components.Shared;
using Estimator.Data.Entities;
using Estimator.Data.Models;
using Estimator.Data.Tenant;
using Mapster;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Estimator.Components.PriceAndSize.Footing;

public class MetricFootingReinforcingService(TenantAreaDbContext dbContext, AuthenticationStateProvider authenticationStateProvider, ILogger<MetricFootingReinforcingService> logger)
    : BaseTenantAreaService(dbContext, authenticationStateProvider, logger)
{

    public async Task<List<MetricPricingModel>> GetMetricReinforcingPrices()
    {
        TenantInfo? tenantInfo = await GetTenantInfo();

        if (tenantInfo is null)
        {
            return null;
        }

        MetricPricingModel lenghtModel = await GetRebarType(Rebar.Type.FootingLengths, tenantInfo.TenantId);
        MetricPricingModel cross = await GetRebarType(Rebar.Type.FootingCrossPieces, tenantInfo.TenantId);
        MetricPricingModel dowels = await GetRebarType(Rebar.Type.FootingDowels, tenantInfo.TenantId); 

        return [lenghtModel, cross, dowels];
    }

    public async Task<MetricPricingModel> GetMetricFootingReinforcingAsync(Rebar.Type rebarType)
    {

        TenantInfo? tenantInfo = await GetTenantInfo();

        if (tenantInfo is null)
        {
            return null;
        }

        return await GetRebarType(rebarType, tenantInfo.TenantId);

    }

    private async void UpdateTypeSize(string tenantId, Rebar.Type rebarType, Rebar.MetricSize rebarSize, InputPriceModel inputPriceModel)
    {
         MetricReinforcingPrice? entity = await _dBContext.MetricReinforcingPrices
              .Where(c => c.RebarTypeId == rebarType && c.RebarSizeId == rebarSize && c.TenantId == tenantId)
              .FirstOrDefaultAsync();

        if (entity is null)
        {
            //create new
            MetricReinforcingPrice metric = new()
            {
                RebarSizeId = rebarSize,
                RebarTypeId = rebarType,
                PerEach = inputPriceModel.Each,
                PerEachInstall = inputPriceModel.EachInstall,
                PerFootInstall = inputPriceModel.PerFoot,
                TenantId = tenantId
            };

            _dBContext.MetricReinforcingPrices.Add(metric);

            await _dBContext.SaveChangesAsync();
        }
        else
        {
            //update
            entity.PerEach = inputPriceModel.Each;
            entity.PerEachInstall = inputPriceModel.EachInstall;
            entity.PerFootInstall = inputPriceModel.PerFoot;

            await _dBContext.SaveChangesAsync();

        }

    }

    public async Task<MetricPricingModel?> UpdateMetricFootingReinforcingAsync(MetricPricingModel updated)
    {
        
        TenantInfo? tenantInfo = await GetTenantInfo();

        if (tenantInfo is null)
        {
            return null;
        }

        UpdateTypeSize(tenantInfo.TenantId, updated.RebarType, Rebar.MetricSize.TenM, updated.TenM);
        UpdateTypeSize(tenantInfo.TenantId, updated.RebarType, Rebar.MetricSize.FifteenM, updated.FifteenM);
        UpdateTypeSize(tenantInfo.TenantId, updated.RebarType, Rebar.MetricSize.TwentyM, updated.TwentyM);
        UpdateTypeSize(tenantInfo.TenantId, updated.RebarType, Rebar.MetricSize.TwentyFiveM, updated.TwentyFiveM);

        return await GetRebarType(updated.RebarType, tenantInfo.TenantId);


    }

    private async Task<MetricPricingModel> GetRebarType(Rebar.Type type, string tenantId)
    {

        List<MetricReinforcingPrice> rebarTypePrice = await _dBContext.MetricReinforcingPrices
           .Where(r => r.TenantId == tenantId && r.RebarTypeId == type)
           .ToListAsync();

        MetricPricingModel model = new();
        model.RebarType = type;

        if (rebarTypePrice.Count > 0)
        {
            model.TenM.Each = rebarTypePrice.Where(l => l.RebarSizeId == Rebar.MetricSize.TenM).First().PerEach;
            model.TenM.EachInstall = rebarTypePrice.Where(l => l.RebarSizeId == Rebar.MetricSize.TenM).First().PerEachInstall;
            model.TenM.PerFoot = rebarTypePrice.Where(l => l.RebarSizeId == Rebar.MetricSize.TenM).First().PerFootInstall;

            model.FifteenM.Each = rebarTypePrice.Where(l => l.RebarSizeId == Rebar.MetricSize.FifteenM).First().PerEach;
            model.FifteenM.EachInstall = rebarTypePrice.Where(l => l.RebarSizeId == Rebar.MetricSize.FifteenM).First().PerEachInstall;
            model.FifteenM.PerFoot = rebarTypePrice.Where(l => l.RebarSizeId == Rebar.MetricSize.FifteenM).First().PerFootInstall;

            model.TwentyM.Each = rebarTypePrice.Where(l => l.RebarSizeId == Rebar.MetricSize.TwentyM).First().PerEach;
            model.TwentyM.EachInstall = rebarTypePrice.Where(l => l.RebarSizeId == Rebar.MetricSize.TwentyM).First().PerEachInstall;
            model.TwentyM.PerFoot = rebarTypePrice.Where(l => l.RebarSizeId == Rebar.MetricSize.TwentyM).First().PerFootInstall;

            model.TwentyFiveM.Each = rebarTypePrice.Where(l => l.RebarSizeId == Rebar.MetricSize.TwentyFiveM).First().PerEach;
            model.TwentyFiveM.EachInstall = rebarTypePrice.Where(l => l.RebarSizeId == Rebar.MetricSize.TwentyFiveM).First().PerEachInstall;
            model.TwentyFiveM.PerFoot = rebarTypePrice.Where(l => l.RebarSizeId == Rebar.MetricSize.TwentyFiveM).First().PerFootInstall;


        }

        return model;

    }
    
    

}
