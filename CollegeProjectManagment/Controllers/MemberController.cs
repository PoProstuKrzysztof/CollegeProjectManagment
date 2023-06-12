using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.DTO;
using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Core.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollegeProjectManagment.Controllers;

[Route("api/member")]
[ApiController]
public class MemberController : ControllerBase
{
    private IRepositoryWrapper _repository;
    private Mapper _mapper = new Mapper();

    public MemberController(IRepositoryWrapper repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllMembers()
    {
        try
        {
            var members = await _repository.Member.GetAllMembers();

            if (!members.Any())
            {
                return NotFound();
            }

            return Ok(members.Select(m => _mapper.MapMemberToMemberDTO(m)).ToList());
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}", Name = "MemberById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMemberById(int id)
    {
        try
        {
            var member = await _repository.Member.GetMemberById(id);

            return member == null ? NotFound() : Ok(_mapper.MapMemberToMemberDTO(member));
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [Authorize(Roles = "leader")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateMember([FromBody] MemberDTO member)
    {
        try
        {
            if (member is null)
            {
                return BadRequest("Member object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            _repository.Member.CreateMember(_mapper.MapMemberDtoToMember(member));
            await _repository.Save();

            return CreatedAtRoute("MemberById", new { id = member.Id }, member);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [Authorize(Roles = "leader")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateMember([FromBody] MemberDTO member)
    {
        try
        {
            if (member is null)
            {
                return BadRequest("Member object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var memberEntity = await _repository.Member.GetMemberById(member.Id);
            if (memberEntity is null)
            {
                return NotFound();
            }

            _repository.Member.UpdateMember(_mapper.MapMemberDtoToMember(member));
            await _repository.Save();

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [Authorize(Roles = "leader")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteMember(int id)
    {
        try
        {
            var member = await _repository.Member.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }

            _repository.Member.DeleteMember(member);
            _repository.Save();

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("getByTechnology")]
    public async Task<IActionResult> GetMembersByTechnology(string? tech)
    {
        try
        {
            var members = await _repository.Member.GetAllMembersByTechnology(tech);
            if (!members.Any())
            {
                return NotFound();
            }

            return Ok(members.Select(m => _mapper.MapMemberToMemberDTO(m)).ToList());
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("{teamId}/SendSubmission/{memberId}")]
    public async Task<IActionResult> SendSubbmision(int teamId, int memberId)
    {
        try
        {
            var team = await _repository.Team.GetTeamById(teamId);
            if (team == null)
            {
                return BadRequest("Team doesn't exist");
            }

            var member = await _repository.Member.GetMemberById(memberId);
            if (member == null)
            {
                return BadRequest("Member doesn't exist");
            }

            var subbmision = new ProjectSubmissionDTO()
            {
                TeamId = teamId,
                SubbmisionerId = memberId
            };

            if (team.ProjectSubbmisions == null)
            {
                team.ProjectSubbmisions = new List<ProjectSubmissionDTO>();
            }

            team.ProjectSubbmisions.Add(subbmision);

            _repository.Team.UpdateTeam(team);
            await _repository.Save();

            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}