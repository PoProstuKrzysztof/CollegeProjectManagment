using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.Enums;
using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly RepositoryContext _context;

    public ProjectRepository(RepositoryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Project>> GetProjects()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<Project> GetProject(int id)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task<Project> CreateProject(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task UpdateProject(int id, Project project)
    {
        if (id != project.Id)
        {
            throw new ArgumentException("Invalid project ID");
        }

        _context.Entry(project).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            throw new ArgumentException("Project not found");
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateProjectStatusAndLink(int id, ProjectState state, string repositoryLink)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            throw new ArgumentException("Project not found");
        }

        if (state != ProjectState.Finished && !string.IsNullOrWhiteSpace(repositoryLink))
        {
            throw new InvalidOperationException("Cannot add or update repository link until project is finished");
        }

        project.State = state;
        project.RepositoryLink = repositoryLink;

        _context.Entry(project).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
//public ProjectRepository(RepositoryContext repositoryContext) : base(repositoryContext)
//    {

//    }
//}