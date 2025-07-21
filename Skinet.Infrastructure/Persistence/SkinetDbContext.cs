using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Persistence
{
    public  class SkinetDbContext : DbContext
    {
        public SkinetDbContext(DbContextOptions<SkinetDbContext> options) : base(options)
        {      
        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

           
            base.OnModelCreating(modelBuilder);
          
        }
    }
   
}
