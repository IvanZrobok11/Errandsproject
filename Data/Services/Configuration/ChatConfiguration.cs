using System;
using System.Collections.Generic;
using System.Text;
using Errands.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Errands.Data.Services.Configuration
{
    public class ChatConfiguration : IEntityTypeConfiguration<UserChat>
    {
        public void Configure(EntityTypeBuilder<UserChat> builder)
        {
            builder.HasKey(k => new {k.UserId, k.ChatId});

            builder.HasOne(uch => uch.Interlocutor)
                .WithMany(u => u.UserChats)
                .HasForeignKey(uch => uch.UserId);

            builder.HasOne(uch => uch.Chat)
                .WithMany(u => u.UsersChat)
                .HasForeignKey(uch => uch.ChatId);
        }
    }
}
