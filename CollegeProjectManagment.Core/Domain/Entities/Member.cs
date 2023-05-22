using CollegeProjectManagment.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Domain.Entities;

public class Member
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; }
    public Role Role { get; set; }
    public List<Project> Projects { get; set; }
    public List<string> KnownTechnologies { get; set; } // można dodać tutaj tez enuma
    public Dictionary<string, int> SkillRatings { get; set; }
}