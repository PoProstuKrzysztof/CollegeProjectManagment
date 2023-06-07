using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.DTO;

public class TeamDTO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public bool IsOpen { get; set; } = false;
}