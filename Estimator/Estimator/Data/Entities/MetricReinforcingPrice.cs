using System;
using Microsoft.EntityFrameworkCore;

namespace Estimator.Data.Entities;

public class MetricReinforcingPrice : TenantBase
{
    public int Id { get; set; }
    public Rebar.Type RebarTypeId { get; set; }
    public Rebar.MetricSize RebarSizeId { get; set; }
    public decimal PerEach { get; set; }
    public decimal PerFootInstall { get; set; }
    public decimal PerEachInstall { get; set; }

}



