using System;
using Estimator.Data.Entities;

namespace Estimator.Data.Models;

public class MetricPricingModel
{
    public int Id {get; set;} = -1;

    public Rebar.Type RebarType { get; set; }

    public InputPriceModel TenM { get; set; } = new();

    public InputPriceModel FifteenM { get; set; }= new();

    public InputPriceModel TwentyM { get; set; }= new();

    public InputPriceModel TwentyFiveM { get; set; }= new();

}


public class InputPriceModel
    {
        public decimal Each;
        public decimal PerFoot;
        public decimal EachInstall;
    }
