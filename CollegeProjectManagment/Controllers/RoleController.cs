using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.DTO;
using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Core.Mapper;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom.Compiler;

namespace CollegeProjectManagment.Controllers;

[Route("api/role")]
[ApiController]
public class RoleController : ControllerBase
{
    private IRepositoryWrapper _repository;
    private Mapper _mapper = new Mapper();

    public RoleController(IRepositoryWrapper repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllRoles()
    {
        try
        {
            var roles = await _repository.Role.GetAllRoles();

            if (!roles.Any())
            {
                return NotFound();
            }

            return Ok(roles.Select(r => _mapper.MapRoleToRoleDTO(r)).ToList());
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

            return role == null ? NotFound() : Ok(_mapper.MapRoleToRoleDTO(role));
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
    public IActionResult CreateRole([FromBody] RoleDTO role)
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

            _repository.Role.CreateRole(_mapper.MapRoletDtoToProject(role));
            _repository.Save();

            return CreatedAtRoute("RoleById", new { id = role.Id }, role);
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
    public async Task<IActionResult> UpdateRole([FromBody] RoleDTO role)
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

            var roleEntity = await _repository.Role.GetRoleById(role.Id);
            if (roleEntity is null)
            {
                return NotFound();
            }

            _repository.Role.UpdateRole(_mapper.MapRoletDtoToProject(role));
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
    public async Task<IActionResult> DeleteOwner(int id)
    {
        try
        {
            var role = await _repository.Role.GetRoleById(id);
            if (role == null)
            {
                return NotFound();
            }

            if (_repository.Member.MembersByRole(id).Any())
            {
                return BadRequest("Cannot delete role. It has related members. Delete those members first");
            }

            _repository.Role.DeleteRole(role);
            await _repository.Save();

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}