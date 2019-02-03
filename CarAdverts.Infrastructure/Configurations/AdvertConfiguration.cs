using System;
using System.Collections.Generic;
using System.Text;
using CarAdverts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAdverts.Infrastructure.Configurations
{
    public class AdvertConfiguration : IEntityTypeConfiguration<Advert>
    {
        public void Configure(EntityTypeBuilder<Advert> builder)
        {
            builder.Property(e => e.Id).UseSqlServerIdentityColumn().ValueGeneratedOnAdd();
            builder.Property(e => e.Title).IsRequired().HasMaxLength(256);
            builder.Property(e => e.Fuel).IsRequired().HasColumnType("tinyint");
            builder.Property(e => e.Price).IsRequired();
            builder.Property(e => e.IsNew).IsRequired();
        }
    }
}
