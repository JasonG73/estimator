using System;
using System.ComponentModel.DataAnnotations;

namespace Estimator.Data.Entities;

public class Company : TenantBase
{
    public int Id { get; set; } = -1;
    public string Name { get; set; } = string.Empty;
    public string Street {get; set;} = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

}
