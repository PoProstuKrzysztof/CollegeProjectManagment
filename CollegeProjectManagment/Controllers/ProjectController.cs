﻿using CollegeProjectManagment.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Core.Mapper;
using CollegeProjectManagment.Core.DTO;

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

            _repository.Project.CreateProject(_mapper.MapyProjectDtoToProject(project));
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

            _repository.Project.UpdateProject(_mapper.MapyProjectDtoToProject(project));
            await _repository.Save();

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
}