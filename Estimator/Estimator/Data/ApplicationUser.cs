using Microsoft.AspNetCore.Identity;

namespace Estimator.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public required string TenantId { get; set; }

    [PersonalData]
    public required string Address { get; set; }

        
}
