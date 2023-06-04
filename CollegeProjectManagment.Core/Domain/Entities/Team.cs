using CollegeProjectManagment.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Domain.Entities;

[Table("team")]
public class Team
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    [Column("TeamId")]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public bool IsOpen { get; set; } = false;

    //Relationships

    public ICollection<Project> CompletedProjects { get; set; }

    public ICollection<Member> Members { get; set; }

    public ICollection<Project> Projects { get; set; }
}