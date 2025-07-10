using System;
using System.ComponentModel.DataAnnotations;

namespace Estimator.Data.Entities;

public abstract class PriceAndSizeBase
{
    public int Id {get; init;}
    public string TenantId { get; set; } = string.Empty;
    public string? Code { get; set; } = string.Empty;        
    public bool IncludeDescriptionInProposal { get; init; }
    
    public double Height {get; set;}
    public double Width { get; set;}
    public double Length {get; set;}
}
