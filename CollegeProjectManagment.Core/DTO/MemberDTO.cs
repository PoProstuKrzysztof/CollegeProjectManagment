using CollegeProjectManagment.Core.Enums;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.DTO;

public class MemberDTO
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; } = string.Empty;

    [Required]
    public string? Surname { get; set; } = string.Empty;

    [JsonConverter(typeof(StringEnumConverter))]
    public ICollection<ProgrammingLanguages>? KnownTechnologies { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public ICollection<SkillRating>? SkillRatings { get; set; }
}