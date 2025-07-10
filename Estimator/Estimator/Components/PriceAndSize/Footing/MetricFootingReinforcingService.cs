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

public class MetricFootingReinforcingService(TenantAreaDbContext dbContext, AuthenticationStateProvider authenticationStateProvider)
    : BaseTenantAreaService(dbContext, authenticationStateProvider)
{

    public async Task<List<MetricPricingModel>> GetMetricReinforcingPrices()
    {
        var tenantId = await GetTenantId();

        if (string.IsNullOrEmpty(tenantId))
        {
            throw new Exception("error with tenantId");
        }

        MetricPricingModel lenghtModel = await GetRebarType(Rebar.Type.FootingLengths, tenantId);
        MetricPricingModel cross = await GetRebarType(Rebar.Type.FootingCrossPieces, tenantId);
        MetricPricingModel dowels = await GetRebarType(Rebar.Type.PadDowels, tenantId); 

        return [lenghtModel, cross, dowels];
    }

    public async Task<MetricPricingModel> GetMetricFootingAsync(int id)
    {

        var tenantId = await GetTenantId();

        if (string.IsNullOrEmpty(tenantId))
        {
            throw new Exception("error with tenantId");
        }

        return new MetricPricingModel();

       /*  return await _dBContext.MetricReinforcingPrices
            .Where(c => c.Id == id && c.TenantId == tenantId)
            .FirstOrDefaultAsync(); */
    }

    public async Task<bool> UpdateMetricFootingAsync(int id, MetricPricingModel updated)
    {
        var tenantId = await GetTenantId();

        if (string.IsNullOrEmpty(tenantId))
        {
            throw new Exception("error with tenantId");
        }
        
        

       /*  MetricReinforcingPrice entity = await _dBContext.MetricReinforcingPrices
              .Where(c => c.Id == id && c.TenantId == tenantId)
              .FirstOrDefaultAsync(); */



        //updated.Adapt(entity);

        await _dBContext.SaveChangesAsync();


        return true;


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
