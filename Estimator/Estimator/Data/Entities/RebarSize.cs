using System;

namespace Estimator.Data.Entities;

public class RebarSize
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Size { get; set; } = string.Empty;
}
