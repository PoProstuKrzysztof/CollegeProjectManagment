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

public record class MemberDTO
{
    public int Id { get; set; }

    public string? Name { get; set; } = string.Empty;

    public string? Surname { get; set; } = string.Empty;


    public ICollection<ProgrammingLanguages>? KnownTechnologies { get; set; }

    public ICollection<SkillRating>? SkillRatings { get; set; }
}