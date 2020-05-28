using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using PhilatelistOnMVC.Models;

namespace MyProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MyProject.Models.Mark> Mark { get; set; }
        public DbSet<PhilatelistOnMVC.Models.Philatelist> Philatelist { get; set; }
    }
}
