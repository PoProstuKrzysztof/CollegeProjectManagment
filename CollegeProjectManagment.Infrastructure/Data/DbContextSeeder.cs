using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.Enums;
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
        var member = new Member
        {
            Id = 1,
            Name = "Krzysztof ",
            Surname = "Palonek",
            SkillRatings = new List<SkillRating> { SkillRating.Expert },
            KnownTechnologies = new List<ProgrammingLanguages> { ProgrammingLanguages.CSharp, ProgrammingLanguages.JavaScript }
        };

        var team = new Team
        {
            Id = 1,
            IsOpen = true,
            Name = "Backend"
        };

        var role = new Role
        {
            Id = 1,
            Name = "Programista"
        };

        var project = new Project
        {
            Id = 1,
            Title = "Testowy projekt",
            State = ProjectState.Started,
            TechnologyStack = "C#",
            PlannedEndDate = new DateTime(2023, 10, 23),
            CompletionDate = DateTime.Now,
            RepositoryLink = "https://github/Po_prostu_krzysztof",
            Requirements = "Be human",
            Description = "We are doing just fine",
            NumberOfMembers = 3,
            AssignedTeamId = 1
        };

        context.Projects.Add(project);
        context.Teams.Add(team);
        context.Members.Add(member);
        context.Roles.Add(role);

        context.SaveChanges();
    }
}