using Assignment_7.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Assignment_7.Data
{
    public class Assignment_5Context : IdentityDbContext
    {
        public Assignment_5Context (DbContextOptions<Assignment_5Context> options)
            : base(options)
        {
        }

        public DbSet<Assignment_7.Models.Movie> Movie { get; set; } = default!;
    
    }
}
