﻿// <auto-generated />
using System;
using Manage_tasks_Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Manage_tasks_Database.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20231012161256_AddedUserSalt")]
    partial class AddedUserSalt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Manage_tasks_Database.Entities.ProjectEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Projects", (string)null);
                });

            modelBuilder.Entity("Manage_tasks_Database.Entities.ProjectTeamEntity", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProjectId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("Projects_Teams", (string)null);
                });

            modelBuilder.Entity("Manage_tasks_Database.Entities.TaskEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AssignedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("FinishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte>("StatusId")
                        .HasColumnType("tinyint");

                    b.Property<Guid?>("TaskListId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AssignedUserId");

                    b.HasIndex("TaskListId");

                    b.ToTable("Tasks", (string)null);
                });

            modelBuilder.Entity("Manage_tasks_Database.Entities.TaskListEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Task_Lists", (string)null);
                });

            modelBuilder.Entity("Manage_tasks_Database.Entities.TeamEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid?>("LeaderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("LeaderId");

                    b.ToTable("Teams", (string)null);
                });

            modelBuilder.Entity("Manage_tasks_Database.Entities.TeamUserEntity", b =>
                {
                    b.Property<Guid>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TeamId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Teams_Users", (string)null);
                });

            modelBuilder.Entity("Manage_tasks_Database.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Position")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Manage_tasks_Database.Entities.ProjectTeamEntity", b =>
                {
                    b.HasOne("Manage_tasks_Database.Entities.ProjectEntity", null)
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Manage_tasks_Database.Entities.TeamEntity", null)
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Manage_tasks_Database.Entities.TaskEntity", b =>
                {
                    b.HasOne("Manage_tasks_Database.Entities.UserEntity", "AssignedUser")
                        .WithMany("Tasks")
                        .HasForeignKey("AssignedUserId");

                    b.HasOne("Manage_tasks_Database.Entities.TaskListEntity", "TaskList")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskListId");

                    b.Navigation("AssignedUser");

                    b.Navigation("TaskList");
                });

            modelBuilder.Entity("Manage_tasks_Database.Entities.TaskListEntity", b =>
                {
                    b.HasOne("Manage_tasks_Database.Entities.ProjectEntity", "Project")
                        .WithMany("TaskLists")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Manage_tasks_Database.Entities.TeamEntity", b =>
                {
                    b.HasOne("Manage_tasks_Database.Entities.UserEntity", "Leader")
                        .WithMany("TeamsLeader")
                        .HasForeignKey("LeaderId");

                    b.Navigation("Leader");
                });

            modelBuilder.Entity("Manage_tasks_Database.Entities.TeamUserEntity", b =>
                {
                    b.HasOne("Manage_tasks_Database.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Manage_tasks_Database.Entities.TeamEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Manage_tasks_Database.Entities.ProjectEntity", b =>
                {
                    b.Navigation("TaskLists");
                });

            modelBuilder.Entity("Manage_tasks_Database.Entities.TaskListEntity", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Manage_tasks_Database.Entities.UserEntity", b =>
                {
                    b.Navigation("Tasks");

                    b.Navigation("TeamsLeader");
                });
#pragma warning restore 612, 618
        }
    }
}
