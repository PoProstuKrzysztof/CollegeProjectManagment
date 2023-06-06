using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Interfaces;

public interface IProjectRepository //: IRepositoryBase<Project>
{
    Task<IEnumerable<Project>> GetProjects();
    Task<Project> GetProject(int id);
    Task<Project> CreateProject(Project project);
    Task UpdateProject(int id, Project project);
    Task DeleteProject(int id);
    Task UpdateProjectStatusAndLink(int id, ProjectState state, string repositoryLink);
}