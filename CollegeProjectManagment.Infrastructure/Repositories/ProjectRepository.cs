using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Infrastructure.Repositories;

public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
{
    public ProjectRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}