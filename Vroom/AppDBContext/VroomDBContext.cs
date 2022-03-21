using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Vroom.Models;
using Microsoft.AspNetCore.Identity;

namespace Vroom.AppDBContext
{
    public class VroomDBContext:IdentityDbContext<IdentityUser>
    {
        public VroomDBContext(DbContextOptions<VroomDBContext> options)
            :base(options)
        {
        
        }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Bike> Bikes { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}
