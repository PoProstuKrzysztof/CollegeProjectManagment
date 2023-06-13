using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Interfaces;

public interface IProjectRepository : IRepositoryBase<Project>
{
    //BASIC CRUD
    Task<IEnumerable<Project>> GetAllProjects();

    Task<Project> GetProjectById(int id);

    void CreateProject(Project project);

    void UpdateProject(Project project);

    void DeleteProject(Project project);
    Task<DifficultyLevel> ReturnDifficultyLevel(int id);
    Task<bool> UpdateProjectStatus(int id, string command);
}