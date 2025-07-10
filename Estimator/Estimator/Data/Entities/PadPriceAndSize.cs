using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estimator.Data.Entities;

public class PadPriceAndSize : PriceAndSizeBase
{
    public decimal LabourPerEach { get; set; }

   [NotMapped]
    public double CubicFoot
    {
        get
        {
            return Math.Round((Width / 12) * (Height / 12) * (Length / 12), 2);
        }
    }

    [NotMapped]
    public string Description

    {
        get
        {
            return $"pad @ {Width}'' x {Length}'' x {Height}'' thick";
        }

    } 

}
