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
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    [Column("MemberId")]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Surname { get; set; } = string.Empty;

    public ICollection<ProgrammingLanguages>? KnownTechnologies { get; set; }

    public ICollection<SkillRating>? SkillRatings { get; set; }

    // Relationships
    public int RoleId { get; set; }

    public Role? Role { get; set; }

    public int TeamId { get; set; }
    public Team? Team { get; set; }
}