using System;
using System.Collections.Generic;
using System.Text;
using CarAdverts.Core.Entities;
using  Microsoft.EntityFrameworkCore;

namespace CarAdverts.Infrastructure.Data
{
    public class AdvertContext : DbContext
    {
        public AdvertContext(DbContextOptions<AdvertContext> options) : base(options)
        {
        }
        public DbSet<Advert> Adverts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdvertContext).Assembly);
        }


    
    }
}
