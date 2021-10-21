using Errands.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Errands.Data.Services.Configuration
{
    public class ErrandConfiguration : IEntityTypeConfiguration<Errand>
    {
        public void Configure(EntityTypeBuilder<Errand> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Id).HasColumnName("id");

            builder.Property(p => p.Title)
                .HasMaxLength(60)
                .HasColumnName("title")
                .IsRequired();

            builder.Property(p => p.Description).HasColumnName("desc");

            builder.Property(p => p.Cost)
                .HasDefaultValue(0)
                .HasColumnName("cost");

            builder.Property(p => p.Active).HasDefaultValue(true).HasColumnName("active");

            builder.Property(p => p.Done).HasDefaultValue(false).HasColumnName("done");

            builder.HasOne(e => e.User).WithMany(e => e.Errands)
                .HasForeignKey(k => k.UserId)
                .HasConstraintName("FK_User_Errand");
            builder.Property(p => p.HelperUserId).HasColumnName("helper_user_id");
            
        }
    }
}
