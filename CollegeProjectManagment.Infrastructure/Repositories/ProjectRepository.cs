using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.Enums;
using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        return await FindAll().
            OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<Project> GetProjectById(int id)
    {
        return await FindByCondition(x => x.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public void CreateProject(Project project)
    {
        Create(project);
    }

    public void UpdateProject(Project project)
    {
        Update(project);
    }

    public void DeleteProject(Project project)
    {
        Delete(project);
    }

    public async Task<bool> UpdateProjectStatus(int id, string? command)
    {
        var projectEntity = await FindByCondition(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        if (projectEntity is null)
        {
            return false;
        }

        if (command is null)
        {
            command = "revert";
        }

        if (command.ToLower().Equals("next"))
        {
            projectEntity.State = projectEntity.State switch
            {
                ProjectState.Created => ProjectState.TeamCompleted,
                ProjectState.TeamCompleted => ProjectState.Started,
                ProjectState.Started => ProjectState.Finished,
                ProjectState.Finished => ProjectState.Testing,
                ProjectState.Testing => ProjectState.Completed,
                _ => throw new ArgumentException("Project is finished")
            };
            Update(projectEntity);
        }
        else
        {
            projectEntity.State = projectEntity.State switch
            {
                ProjectState.Completed => ProjectState.Testing,
                ProjectState.Testing => ProjectState.Finished,
                ProjectState.Finished => ProjectState.Started,
                ProjectState.Started => ProjectState.TeamCompleted,
                ProjectState.TeamCompleted => ProjectState.Created,
                _ => throw new ArgumentException("Project doesn't have state")
            };

            Update(projectEntity);
        }

        return true;
    }
}