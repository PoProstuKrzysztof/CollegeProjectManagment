using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Infrastructure.Data;

public class DbContextSeeder
{
    public static void SeedData(RepositoryContext context)
    {
        if (context.Members.Any())
        {
            context.Members.RemoveRange(context.Members);
            context.Teams.RemoveRange(context.Teams);
            context.Roles.RemoveRange(context.Roles);
            context.Projects.RemoveRange(context.Projects);
            context.SaveChanges();
        }

        var member = new Member
        {
            Id = 1,
            Name = "Krzysztof ",
            Surname = "Palonek",
            SkillRatings = new List<SkillRating> { SkillRating.Expert },
            KnownTechnologies = new List<ProgrammingLanguages> { ProgrammingLanguages.CSharp, ProgrammingLanguages.JavaScript },
        };

        var team = new Team
        {
            Id = 1,
            IsOpen = true,
            Name = "Backend",
            Members = new List<Member> { member },
            Projects = new List<Project> { }
        };

        var role = new Role
        {
            Id = 1,
            Name = "Programista",
            Members = new List<Member> { member }
        };

        var project = new Project
        {
            Id = 1,
            Title = "Testowy projekt",
            State = ProjectState.Started,
            DifficultyLevel = DifficultyLevel.Hard,
            ProgrammingLanguages = new List<ProgrammingLanguages> { ProgrammingLanguages.Java, ProgrammingLanguages.CSharp },
            TechnologyStack = "C#",
            PlannedEndDate = new DateTime(2023, 10, 23),
            CompletionDate = DateTime.Now,
            RepositoryLink = "https://github/Po_prostu_krzysztof",
            Requirements = "Be human",
            Description = "We are doing just fine",
            NumberOfMembers = 3,
            AssignedTeamId = 1,
            Team = team,
            LeaderId = 1,
            Leader = member
        };

        context.Projects.Add(project);
        context.Teams.Add(team);
        context.Members.Add(member);
        context.Roles.Add(role);

        member.Role = role;
        member.RoleId = 1;
        team.Projects.Add(project);

        context.SaveChanges();
    }
}