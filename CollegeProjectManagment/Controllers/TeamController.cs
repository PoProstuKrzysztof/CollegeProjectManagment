using CollegeProjectManagment.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Core.Mapper;
using CollegeProjectManagment.Core.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Controllers
{
    [ApiController]
    [Route("api/team")]
    public class TeamController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private Mapper _mapper = new Mapper();

        public TeamController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllTeams()
        {
            try
            {
                var teams = await _repository.Team.GetAllTeams();

                if (!teams.Any())
                {
                    return NotFound();
                }

                return Ok(teams.Select(t => _mapper.MapTeamToTeamDTO(t)).ToList());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "TeamById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeamById(int id)
        {
            try
            {
                var team = await _repository.Team.GetTeamById(id);

                return team == null ? NotFound() : Ok(_mapper.MapTeamToTeamDTO(team));
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
        public async Task<IActionResult> CreateTeam([FromBody] TeamDTO team)
        {
            try
            {
                if (team is null)
                {
                    return BadRequest("Teams object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _repository.Team.CreateTeam(_mapper.MapTeamDtoToTeam(team));
                await _repository.Save();

                return CreatedAtRoute("TeamById", new { id = team.Id }, team);
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
        public async Task<IActionResult> UpdateTeam([FromBody] TeamDTO team)
        {
            try
            {
                if (team is null)
                {
                    return BadRequest("Obiekt zespołu jest pusty");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Nieprawidłowy obiekt modelu");
                }

                var existingTeam = await _repository.Team.GetTeamById(team.Id);
                if (existingTeam == null)
                {
                    return NotFound();
                }

                _repository.Team.UpdateTeam(_mapper.MapTeamDtoToTeam(team));
                await _repository.Save();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Wewnętrzny błąd serwera");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            try
            {
                var team = await _repository.Team.GetTeamById(id);
                if (team == null)
                {
                    return NotFound();
                }

                _repository.Team.DeleteTeam(team);
                await _repository.Save();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Wewnętrzny błąd serwera");
            }
        }
    }
}