using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CollegeProjectManagment.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Infrastructure.Data;

public class RepositoryContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public RepositoryContext(DbContextOptions options) : base(options)
    {
    }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Member> Members { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Team> Teams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Project>()
            .Property(p => p.ProgrammingLanguages)
            .HasConversion(v => string.Join(",", v.Select(e => e.ToString("D")).ToArray()),
          v => v.Split(new[] { ',' })
            .Select(e => Enum.Parse(typeof(ProgrammingLanguages), e))
            .Cast<ProgrammingLanguages>()
            .ToList()
            );

        modelBuilder
           .Entity<Member>()
           .Property(p => p.SkillRatings)
           .HasConversion(v => string.Join(",", v.Select(e => e.ToString("D")).ToArray()),
          v => v.Split(new[] { ',' })
            .Select(e => Enum.Parse(typeof(SkillRating), e))
            .Cast<SkillRating>()
            .ToList()
            );

        modelBuilder
            .Entity<Member>()
            .Property(p => p.KnownTechnologies)
            .HasConversion(v => string.Join(",", v.Select(e => e.ToString("D")).ToArray()),
          v => v.Split(new[] { ',' })
            .Select(e => Enum.Parse(typeof(ProgrammingLanguages), e))
            .Cast<ProgrammingLanguages>()
            .ToList()

            );

        modelBuilder.Entity<Project>()
            .HasOne(x => x.Team)
            .WithMany(x => x.Projects)
            .HasForeignKey(k => k.AssignedTeamId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .IsRequired();

        modelBuilder.Entity<Team>()
            .HasMany(x => x.Projects)
            .WithOne(x => x.Team)
            .HasForeignKey(x => x.AssignedTeamId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .IsRequired();
    }
}