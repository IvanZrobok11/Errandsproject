using Errands.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Errands.Data.Services.Configuration
{
    public class FileModelConfiguration : IEntityTypeConfiguration<FileModel>
    {
        public void Configure(EntityTypeBuilder<FileModel> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Id).HasColumnName("id");

            builder.Property(p => p.Path)
                .HasColumnName("path");

            builder.Property(p => p.Name).HasColumnName("nameFile");

            builder.HasOne(e => e.Errand).WithMany(e => e.FileModels)
                .HasForeignKey(k => k.ErrandId);

            builder.Property(p => p.Type).HasConversion(
                v => v.ToString(),
                v => (TypeFile)Enum.Parse(typeof(TypeFile), v));
        }
    }
}
