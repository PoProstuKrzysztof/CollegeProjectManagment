using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Interfaces;

internal interface IRepositoryWrapper
{
    IMemberRepository Member { get; }
    ITeamRepository Team { get; }
    IProjectRepository Project { get; }

    IRoleRepository Role { get; }

    void Save();
}