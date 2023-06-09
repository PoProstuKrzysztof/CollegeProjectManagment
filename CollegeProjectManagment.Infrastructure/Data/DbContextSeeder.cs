using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Infrastructure.Data;

public static class DbContextSeeder
{
    public static void SeedData(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<RepositoryContext>();

            if (!dbContext.Members.Any())
            {
                SeedRoles(dbContext);
                SeedTeams(dbContext);
                SeedMembers(dbContext);
                SeedProjects(dbContext);
            }
        }
    }

    private static void SeedRoles(RepositoryContext context)
    {
        var roles = new List<Role>
        {
            new Role { Id = 1, Name = "Developer" },
            new Role { Id = 2, Name = "Tester" },
            new Role { Id = 3, Name = "Leader" },
            new Role { Id = 4, Name = "DevOps" }
        };

        context.Roles.AddRange(roles);
        context.SaveChanges();
    }

    private static void SeedTeams(RepositoryContext context)
    {
        var teams = new List<Team>
        {
            new Team { Id = 1, Name = "Team A", IsOpen = true },
            new Team { Id = 2, Name = "Team B", IsOpen = false },
            new Team { Id = 3, Name = "Team C", IsOpen = true }
        };

        context.Teams.AddRange(teams);
        context.SaveChanges();
    }

    private static void SeedMembers(RepositoryContext context)
    {
        var members = new List<Member>
        {
            new Member
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                KnownTechnologies = new List<ProgrammingLanguages> { ProgrammingLanguages.Java, ProgrammingLanguages.CSharp },
                SkillRatings = new List<SkillRating> { SkillRating.Beginner, SkillRating.Intermediate },
                Email = "john.doe@gmail.com",
                RoleId = 1,
                TeamId = 1
            },
            new Member
            {
                Id = 2,
                Name = "Jane",
                Surname = "Smith",
                Email = "jane.smith@gmail.com",
                KnownTechnologies = new List<ProgrammingLanguages> { ProgrammingLanguages.Python, ProgrammingLanguages.JavaScript },
                SkillRatings = new List<SkillRating> { SkillRating.Intermediate, SkillRating.Expert },
                RoleId = 2,
                TeamId = 1
            },
            new Member
            {
                Id = 3,
                Name = "Mike",
                Surname = "Johnson",
                KnownTechnologies = new List<ProgrammingLanguages> { ProgrammingLanguages.CSharp },
                SkillRatings = new List<SkillRating> { SkillRating.Beginner },
                RoleId = 3,
                TeamId = 2
            }
        };

        context.Members.AddRange(members);
        context.SaveChanges();
    }

    private static void SeedProjects(RepositoryContext context)
    {
        var projects = new List<Project>
        {
            new Project
            {
                Id = 1,
                Title = "Project 1",
                Description = "Description for Project 1",
                Requirements = "Requirements for Project 1",
                TechnologyStack = "Technology Stack for Project 1",
                ProgrammingLanguages = new List<ProgrammingLanguages> { ProgrammingLanguages.Java, ProgrammingLanguages.CSharp },
                DifficultyLevel = DifficultyLevel.Medium,
                State = ProjectState.Completed,
                PlannedEndDate = new DateTime(2023, 5, 1),
                CompletionDate = new DateTime(2023, 5, 15),
                RepositoryLink = "Repository Link for Project 1",
                AssignedTeamId = 1,
                LeaderId = 1
            },
            new Project
            {
                Id = 2,
                Title = "Project 2",
                Description = "Description for Project 2",
                Requirements = "Requirements for Project 2",
                TechnologyStack = "Technology Stack for Project 2",
                ProgrammingLanguages = new List<ProgrammingLanguages> { ProgrammingLanguages.Python, ProgrammingLanguages.JavaScript },
                DifficultyLevel = DifficultyLevel.Hard,
                State = ProjectState.Started,
                PlannedEndDate = new DateTime(2023, 6, 1),
                RepositoryLink = "Repository Link for Project 2",
                AssignedTeamId = 1,
                LeaderId = 2
            }
        };

        context.Projects.AddRange(projects);
        context.SaveChanges();
    }
}