using CollegeProjectManagment.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Core.Mapper;
using CollegeProjectManagment.Core.DTO;
using CollegeProjectManagment.Core.Enums;
using Microsoft.IdentityModel.Tokens;

namespace CollegeProjectManagment.Controllers;

[ApiController]
[Route("api/project")]
public class ProjectController : ControllerBase
{
    private IRepositoryWrapper _repository;
    private Mapper _mapper = new Mapper();

    public ProjectController(IRepositoryWrapper repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllProjects()
    {
        try
        {
            var projects = await _repository.Project.GetAllProjects();

            if (!projects.Any())
            {
                return NotFound();
            }

            return Ok(projects.Select(p => _mapper.MapProjectToProjectDTO(p)).ToList());
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}", Name = "ProjectById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProjectById(int id)
    {
        try
        {
            var project = await _repository.Project.GetProjectById(id);

            return project == null ? NotFound() : Ok(_mapper.MapProjectToProjectDTO(project));
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProject([FromBody] ProjectDTO project)
    {
        try
        {
            if (project is null)
            {
                return BadRequest("Project object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var projectEntity = _mapper.MapyProjectDtoToProject(project);
            _repository.Project.CreateProject(projectEntity);
            projectEntity.CountMembers(await _repository.Member.CountMembersOfTeam(project.AssignedTeamId));

            await _repository.Save();

            return CreatedAtRoute("ProjectById", new { id = project.Id }, project);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProject([FromBody] ProjectDTO project)
    {
        try
        {
            if (project is null)
            {
                return BadRequest("Project object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var existingProject = await _repository.Project.GetProjectById(project.Id);
            if (existingProject == null)
            {
                return NotFound();
            }

            var projectEntity = _mapper.MapyProjectDtoToProject(project);
            if (projectEntity.State != ProjectState.Finished)
            {
                projectEntity.RepositoryLink = existingProject.RepositoryLink;
            }
            _repository.Project.UpdateProject(projectEntity);
            projectEntity.CountMembers(await _repository.Member.CountMembersOfTeam(project.AssignedTeamId));
            await _repository.Save();

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteProject(int id)
    {
        try
        {
            var project = await _repository.Project.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }

            _repository.Project.DeleteProject(project);
            await _repository.Save();

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Move project state to next state
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("id/NextState")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MoveProjectToNextStateAsync(int id, [FromQuery] string? command)
    {
        try
        {
            if (!await _repository.Project.UpdateProjectStatus(id, command))
            {
                return BadRequest("Couldn't move project to other state");
            }

            var project = await _repository.Project.GetProjectById(id);

            if (project.State == ProjectState.Completed)
            {
                var team = await _repository.Team.GetTeamById(project.AssignedTeamId);

                // Check for completed projects
                team.CompletedProjects.Add(await _repository.Project.GetProjectById(id));
                await _repository.Save();
            }
            var members = await _repository.Member.FindAllMembersOfTeam(project.AssignedTeamId);

            //Manage points if projects move to the next state
            _repository.Member.ManagePoints(members, command);

            await _repository.Save();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("{id}/AddRepositoryLink")]
    public async Task<IActionResult> AddRepositoryLink(int id, string? link)
    {
        try
        {
            if (link.IsNullOrEmpty())
            {
                return BadRequest("You have to put repository link!");
            }

            var project = await _repository.Project.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }

            if (project.State != ProjectState.Completed)
            {
                return BadRequest("Your project is not completed");
            }

            project.RepositoryLink = link;
            await _repository.Save();

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}