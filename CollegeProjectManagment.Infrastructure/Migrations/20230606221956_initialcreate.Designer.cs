﻿// <auto-generated />
using System;
using CollegeProjectManagment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CollegeProjectManagment.Infrastructure.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20230606221956_initialcreate")]
    partial class initialcreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CollegeProjectManagment.Core.Domain.Entities.Member", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("MemberId");

                    b.Property<string>("KnownTechnologies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("SkillRatings")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("TeamId");

                    b.ToTable("member");
                });

            modelBuilder.Entity("CollegeProjectManagment.Core.Domain.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("ProjectId");

                    b.Property<int?>("AssignedTeamId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DifficultyLevel")
                        .HasColumnType("int");

                    b.Property<int?>("LeaderId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfMembers")
                        .HasColumnType("int");

                    b.Property<DateTime>("PlannedEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProgrammingLanguages")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepositoryLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Requirements")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<string>("TechnologyStack")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssignedTeamId");

                    b.HasIndex("LeaderId");

                    b.HasIndex("TeamId");

                    b.ToTable("project");
                });

            modelBuilder.Entity("CollegeProjectManagment.Core.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("RoleId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("role");
                });

            modelBuilder.Entity("CollegeProjectManagment.Core.Domain.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("TeamId");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("team");
                });

            modelBuilder.Entity("CollegeProjectManagment.Core.Domain.Entities.Member", b =>
                {
                    b.HasOne("CollegeProjectManagment.Core.Domain.Entities.Role", "Role")
                        .WithMany("Members")
                        .HasForeignKey("RoleId");

                    b.HasOne("CollegeProjectManagment.Core.Domain.Entities.Team", "Team")
                        .WithMany("Members")
                        .HasForeignKey("TeamId");

                    b.Navigation("Role");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("CollegeProjectManagment.Core.Domain.Entities.Project", b =>
                {
                    b.HasOne("CollegeProjectManagment.Core.Domain.Entities.Team", "Team")
                        .WithMany("Projects")
                        .HasForeignKey("AssignedTeamId")
                        .IsRequired();

                    b.HasOne("CollegeProjectManagment.Core.Domain.Entities.Member", "Leader")
                        .WithMany()
                        .HasForeignKey("LeaderId");

                    b.HasOne("CollegeProjectManagment.Core.Domain.Entities.Team", null)
                        .WithMany("CompletedProjects")
                        .HasForeignKey("TeamId");

                    b.Navigation("Leader");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("CollegeProjectManagment.Core.Domain.Entities.Role", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("CollegeProjectManagment.Core.Domain.Entities.Team", b =>
                {
                    b.Navigation("CompletedProjects");

                    b.Navigation("Members");

                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
