using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.DTO;

public record class ProjectSubmissionDTO
{
    public static int Id { get; set; }

    public int? TeamId { get; set; }
    public int? SubbmisionerId { get; set; }

    public ProjectSubmissionDTO()
    {
        Id++;
    }
}