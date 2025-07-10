using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estimator.Data.Entities;

public class FootingPriceAndSize : PriceAndSizeBase
{
    public decimal LabourCost {get; set;}

    [NotMapped]
    public double SquareFoot
    {
        get
        {
            return Math.Round((Width / 12) * (Height / 12), 4);
        }
    }

    [NotMapped]
    public string Description

    {
        get
        {
            return $"Footing {Width}'' wide x {Height}'' thick";
        }

    }
    
}
