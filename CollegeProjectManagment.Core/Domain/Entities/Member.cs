using CollegeProjectManagment.Core.Enums;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Domain.Entities;

[Table("member")]
public class Member
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
    [Column("MemberId")]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; } = string.Empty;

    [Required]
    public string? Surname { get; set; } = string.Empty;

    /// <summary>
    /// Dodałem maila żeby potem można było łatwiej powiązan usera z członkiem
    /// </summary>
    public string? Email { get; set; }

    public int? PrestigePoints { get; set; } = 0;

    [JsonConverter(typeof(StringEnumConverter))]
    public ICollection<ProgrammingLanguages>? KnownTechnologies { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public ICollection<SkillRating>? SkillRatings { get; set; }

    // Relationships
    public int? RoleId { get; set; }

    public Role? Role { get; set; }

    public int? TeamId { get; set; }
    public Team? Team { get; set; }

    public void AddPoints()
    {
        PrestigePoints += 10;
    }

    public void SubtractPoints()
    {
        if (PrestigePoints != 0)
        {
            PrestigePoints -= 10;
        }
    }
}