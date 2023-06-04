using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Infrastructure.Repositories;

public class RepositoryWrapper
{
    public RepositoryContext _context;
    public IMemberRepository _member;
    public IRoleRepository _role;
    public ITeamRepository _team;
    public IProjectRepository _project;
}