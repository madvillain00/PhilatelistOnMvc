using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhilatelistOnMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() { }
        public string Name { get; set; }
        public string Country { get; set; }
        public string ContactDetails { get; set; }
        public string Availability { get; set; }

    }
}
