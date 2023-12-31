﻿using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manage_tasks_Database.EntityConfigurations;

internal class ProjectConfiguration : IEntityTypeConfiguration<ProjectEntity>
{
    public void Configure(EntityTypeBuilder<ProjectEntity> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(50);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.HasOne(p => p.Team)
            .WithMany(t => t.Projects)
            .HasForeignKey(p => p.TeamId)
            .HasPrincipalKey(t => t.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}