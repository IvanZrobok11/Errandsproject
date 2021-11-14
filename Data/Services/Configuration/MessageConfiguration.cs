using System;
using System.Collections.Generic;
using System.Text;
using Errands.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Errands.Data.Services.Configuration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Id).HasColumnName("id");

            builder.Property(p => p.Content)
                .HasColumnName("message_content")
                .HasMaxLength(1000);

            builder.HasOne(e => e.Chatting).WithMany(e => e.Messages)
                .HasForeignKey(k => k.ChatId)
                .HasConstraintName("FK_Chat_Message");
        }
    }
}
