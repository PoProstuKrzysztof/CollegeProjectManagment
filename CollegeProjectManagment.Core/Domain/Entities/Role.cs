using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Domain.Entities;

[Table("role")]
public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}