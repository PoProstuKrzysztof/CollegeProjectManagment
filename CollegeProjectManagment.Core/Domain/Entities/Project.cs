using CollegeProjectManagment.Core.Enums;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Domain.Entities;

[Table("project")]
public class Project
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
    [Column("ProjectId")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Title for project is required")]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;
    public int NumberOfMembers { get; set; }
    public string TechnologyStack { get; set; } = string.Empty;

    public ICollection<ProgrammingLanguages>? ProgrammingLanguages { get; set; }

    public DifficultyLevel DifficultyLevel { get; set; }

    public ProjectState State { get; set; } = ProjectState.Created;

    public DateTime PlannedEndDate { get; set; }

    public DateTime? CompletionDate { get; set; }
    [RegularExpression(@"^(https?://)?([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$", ErrorMessage = "Invalid repository link")]
    public string RepositoryLink { get; set; } = string.Empty;

    //Relationships

    [AllowNull]
    public int? AssignedTeamId { get; set; }

    public Team? Team { get; set; }
    public int? LeaderId { get; set; }

    public Member? Leader { get; set; }

    public void CountMembers(int count)
    {
        NumberOfMembers = count;
    }
}