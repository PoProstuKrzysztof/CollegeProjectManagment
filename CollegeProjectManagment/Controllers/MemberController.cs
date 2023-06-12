using CollegeProjectManagment.Core.DTO;
using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Core.Mapper;
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

            return Ok(Ok(members.Select(m => _mapper.MapMemberToMemberDTO(m)).ToList()));
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}