using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Domain.Entities;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Member> Members { get; set; }
    public ICollection<Project> Projects { get; set; }
}