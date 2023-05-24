using CollegeProjectManagment.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Domain.Entities;

[Table("member")]
public class Member
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Surname { get; set; } = string.Empty;

    public Role? Role { get; set; }
    public List<Project>? Projects { get; set; }
    public List<string>? KnownTechnologies { get; set; } // można dodać tutaj tez enuma
    public Dictionary<string, int>? SkillRatings { get; set; }
}