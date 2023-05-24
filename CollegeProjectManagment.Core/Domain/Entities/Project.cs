using CollegeProjectManagment.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Domain.Entities;

[Table("project")]
public class Project
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title for the team is required")]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;
    public int NumberOfMembers { get; set; }
    public string TechnologyStack { get; set; } = string.Empty;
    public ICollection<ProgrammingLanguages>? ProgrammingLanguages { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }

    public DateTime PlannedEndDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public ProjectState State { get; set; }
    public string RepositoryLink { get; set; } = string.Empty;
    public ICollection<Team>? Teams { get; set; }

    [ForeignKey(nameof(Member))]
    public int? LeaderId { get; set; }

    public Member? Leader { get; set; }
}