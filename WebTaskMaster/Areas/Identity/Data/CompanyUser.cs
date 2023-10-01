using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebTaskMaster.Areas.Identity.Data;

// Add profile data for application users by adding properties to the CompanyUser class
public class CompanyUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    
    
}

