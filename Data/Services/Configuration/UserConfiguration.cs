using Errands.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Errands.Data.Services.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(k => k.Id);
            entity.Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(25)
                .HasColumnName("user_name");
            

            entity.HasOne(e => e.Logo)
                .WithOne(u => u.User)
                .HasForeignKey<FileModel>(k=>k.UserId);   
        }
    }
}
