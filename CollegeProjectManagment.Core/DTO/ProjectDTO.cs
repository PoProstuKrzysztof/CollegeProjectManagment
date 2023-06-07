using CollegeProjectManagment.Core.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.DTO;

public record class ProjectDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;
    public int NumberOfMembers { get; set; }
    public string TechnologyStack { get; set; } = string.Empty;

    [JsonConverter(typeof(StringEnumConverter))]
    public ICollection<ProgrammingLanguages>? ProgrammingLanguages { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public DifficultyLevel DifficultyLevel { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public ProjectState State { get; set; }

    public DateTime PlannedEndDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public string RepositoryLink { get; set; } = string.Empty;

    public TeamDTO Team { get; set; }
    public MemberDTO Leader { get; set; }
}