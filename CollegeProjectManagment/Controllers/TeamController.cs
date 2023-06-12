using CollegeProjectManagment.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Core.Mapper;
using CollegeProjectManagment.Core.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;
using CollegeProjectManagment.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Roles = "leader")]
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

        [Authorize]
        [HttpGet("{id}/membersOnTeam")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMembersOfTeam([FromRoute] int? id)
        {
            try
            {
                var members = await _repository.Member.FindAllMembersOfTeam(id);
                if (members.Count == 0)
                {
                    return NotFound("There are no members assigned to team");
                }

                return Ok(members.Select(x => _mapper.MapMemberToMemberDTO(x)).ToList());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
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

        [HttpGet("{id}/completedProjects")]
        public async Task<IActionResult> GetCompletedProjects(int id)
        {
            try
            {
                var team = await _repository.Team.GetTeamById(id);
                if (team is null)
                {
                    return NotFound();
                }

                var projects = team.CompletedProjects;
                if (projects is null)
                {
                    return NotFound("There are not completed projects");
                }

                return Ok(projects.Select(x => _mapper.MapProjectToProjectDTO(x)).ToList());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
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

        [Authorize]
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

        /// <summary>
        /// Add member to team, move him from one to another
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpPost("{teamId}/members/{memberId}/moveMemberOtherTeam")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMemberToTeam(int teamId, int memberId)
        {
            try
            {
                var team = await _repository.Team.GetTeamById(teamId);
                if (team == null)
                {
                    return NotFound("Team not found");
                }

                if (!team.IsOpen)
                {
                    return BadRequest("This team is closed. You can't add member.");
                }

                var member = await _repository.Member.GetMemberById(memberId);
                if (member == null)
                {
                    return NotFound("Member not found");
                }

                member.TeamId = teamId;
                if (member.PrestigePoints < 40)
                {
                    member.PrestigePoints = 0;
                }

                _repository.Member.UpdateMember(member);
                await _repository.Save();

                return CreatedAtRoute("TeamById", new { id = member.TeamId }, member);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Close team
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [Authorize]
        [HttpPut("{id}/close")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CloseTeam(int id)
        {
            try
            {
                var team = await _repository.Team.GetTeamById(id);
                if (team == null)
                {
                    return NotFound("Team not found");
                }

                if (!team.IsOpen)
                {
                    return BadRequest("Team is already closed");
                }

                team.IsOpen = false;

                _repository.Team.UpdateTeam(team);
                await _repository.Save();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Open team
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id}/open")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> OpenTeam(int id)
        {
            try
            {
                var team = await _repository.Team.GetTeamById(id);
                if (team == null)
                {
                    return NotFound("Team not found");
                }

                if (team.IsOpen)
                {
                    return BadRequest("Team is already open");
                }

                team.IsOpen = true;

                _repository.Team.UpdateTeam(team);
                await _repository.Save();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}