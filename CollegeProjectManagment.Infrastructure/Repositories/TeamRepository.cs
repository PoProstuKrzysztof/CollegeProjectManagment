using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Infrastructure.Repositories;

public class TeamRepository : RepositoryBase<Team>, ITeamRepository
{
    public TeamRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}