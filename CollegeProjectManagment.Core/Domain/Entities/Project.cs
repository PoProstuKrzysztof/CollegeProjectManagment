using CollegeProjectManagment.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Requirements { get; set; }
    public int NumberOfMembers { get; set; }
    public string TechnologyStack { get; set; }
    public ICollection<ProgrammingLanguages> ProgrammingLanguages { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }
    public DateTime PlannedEndDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public ProjectState State { get; set; }
    public string RepositoryLink { get; set; }
    public ICollection<Team> Teams { get; set; }
}