using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks_Database.Entities
{
    public class Company : IdentityUser
    {
        
        public string? CompanyName { get; set; }

        public string? Country { get; set; }
        public DateTime DateOfBirth { get; set; }

        
  
    }
}
