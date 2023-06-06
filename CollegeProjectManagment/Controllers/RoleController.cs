using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom.Compiler;

namespace CollegeProjectManagment.Controllers;

[Route("api/role")]
[ApiController]
public class RoleController : ControllerBase
{
    private IRepositoryWrapper _repository;

    public RoleController(IRepositoryWrapper repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetAllRoles()
    {
        try
        {
            var roles = _repository.Role.FindAll();

            if (!roles.Any())
            {
                return NotFound();
            }

            return Ok(roles);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}", Name = "RoleById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRolesById(int id)
    {
        try
        {
            var role = await _repository.Role.GetRoleById(id);

            return role == null ? NotFound() : Ok(role);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}/members")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRoleMembersById(int id)
    {
        try
        {
            var role = await _repository.Role.GetRoleWithMembers(id);

            return role == null ? NotFound() : Ok(role);
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
    public async Task<IActionResult> CreateOwner([FromBody] Role role)
    {
        try
        {
            if (role is null)
            {
                return BadRequest("Role object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            _repository.Role.CreateRole(role);
            _repository.Save();

            return CreatedAtRoute("RoleById", new { id = role.Id }, role);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}