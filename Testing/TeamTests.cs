using CollegeProjectManagment.Controllers;
using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.DTO;
using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Core.Mapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Diagnostics;

namespace Testing
{
    [TestFixture]
    public class TeamControllerTests
    {
        private TeamController _teamController;
        private Mock<IRepositoryWrapper> _repositoryWrapperMock;
        private Mapper _mapper;
       
        [SetUp]
        public void Setup()
        {
            _repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            _teamController = new TeamController(_repositoryWrapperMock.Object);
            _mapper = new Mapper();
           
        }

        [Test]
        public async Task GetAllTeams_ReturnsOkResult_WithTeamList()
        {
        
            var teams = new List<Team>
        {
            new Team { Id = 1, Name = "Team 1", IsOpen = true },
            new Team { Id = 2, Name = "Team 2", IsOpen = true },
        };

            _repositoryWrapperMock.Setup(r => r.Team.GetAllTeams()).ReturnsAsync(teams);

        
            var result = await _teamController.GetAllTeams();

        
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOf<List<TeamDTO>>(okResult.Value);
            var teamDTOs = okResult.Value as List<TeamDTO>;
            Assert.AreEqual(teams.Count, teamDTOs.Count);
            Assert.AreEqual(teams[0].Id, teamDTOs[0].Id);
            Assert.AreEqual(teams[0].Name, teamDTOs[0].Name);
            Assert.AreEqual(teams[0].IsOpen, teamDTOs[0].IsOpen);
            Assert.AreEqual(teams[1].Id, teamDTOs[1].Id);
            Assert.AreEqual(teams[1].Name, teamDTOs[1].Name);
            Assert.AreEqual(teams[1].IsOpen, teamDTOs[1].IsOpen);
        }

        [Test]
        public async Task GetAllTeams_ReturnsNotFoundResult_WhenNoTeamsExist()
        {
           
            var teams = new List<Team>();

            _repositoryWrapperMock.Setup(r => r.Team.GetAllTeams()).ReturnsAsync(teams);

        
            var result = await _teamController.GetAllTeams();

          
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetTeamById_ReturnsOkResult_WithTeamDTO()
        {
          
            var teamId = 1;
            var team = new Team { Id = teamId, Name = "Team 1", IsOpen = true };

            _repositoryWrapperMock.Setup(r => r.Team.GetTeamById(teamId)).ReturnsAsync(team);

          
            var result = await _teamController.GetTeamById(teamId);

            
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOf<TeamDTO>(okResult.Value);
            var teamDTO = okResult.Value as TeamDTO;
            Assert.AreEqual(team.Id, teamDTO.Id);
            Assert.AreEqual(team.Name, teamDTO.Name);
            Assert.AreEqual(team.IsOpen, teamDTO.IsOpen);
        }

        [Test]
        public async Task GetTeamById_ReturnsNotFoundResult_WhenTeamDoesNotExist()
        {
           
            var teamId = 1;
            Team team = null;

            _repositoryWrapperMock.Setup(r => r.Team.GetTeamById(teamId)).ReturnsAsync(team);

         
            var result = await _teamController.GetTeamById(teamId);

          
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task CreateTeam_ReturnsCreatedAtRouteResult_WithCreatedTeamDTO()
        {
           
            var teamDTO = new TeamDTO { Id = 1, Name = "Team 1", IsOpen = true };
            var team = _mapper.MapTeamDtoToTeam(teamDTO);

            _repositoryWrapperMock.Setup(r => r.Team.CreateTeam(It.IsAny<Team>()));
            _repositoryWrapperMock.Setup(r => r.Save());

         
            var result = await _teamController.CreateTeam(teamDTO);

          
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            var createdAtRouteResult = result as CreatedAtRouteResult;
            Assert.AreEqual("TeamById", createdAtRouteResult.RouteName);
            Assert.AreEqual(teamDTO.Id, createdAtRouteResult.RouteValues["id"]);
            Assert.AreEqual(teamDTO, createdAtRouteResult.Value);
        }

        [Test]
        public async Task CreateTeam_ReturnsBadRequestResult_WhenTeamDTOIsNull()
        {
        
            TeamDTO teamDTO = null;

        
            var result = await _teamController.CreateTeam(teamDTO);

       
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task CreateTeam_ReturnsBadRequestResult_WhenModelIsInvalid()
        {
           
            var teamDTO = new TeamDTO { Id = 1, Name = string.Empty, IsOpen = true };
            _teamController.ModelState.AddModelError("Name", "The Name field is required.");

         
            var result = await _teamController.CreateTeam(teamDTO);

         
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task UpdateTeam_ReturnsNoContentResult_WhenTeamExists()
        {
           
            var teamDTO = new TeamDTO { Id = 1, Name = "Updated Team", IsOpen = false };
            var existingTeam = new Team { Id = teamDTO.Id, Name = "Team 1", IsOpen = true };

            _repositoryWrapperMock.Setup(r => r.Team.GetTeamById(teamDTO.Id)).ReturnsAsync(existingTeam);
            _repositoryWrapperMock.Setup(r => r.Team.UpdateTeam(It.IsAny<Team>()));
            _repositoryWrapperMock.Setup(r => r.Save());

            var result = await _teamController.UpdateTeam(teamDTO);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task UpdateTeam_ReturnsNotFoundResult_WhenTeamDoesNotExist()
        {
           
            var teamDTO = new TeamDTO { Id = 1, Name = "Updated Team", IsOpen = false };
            Team existingTeam = null;

            _repositoryWrapperMock.Setup(r => r.Team.GetTeamById(teamDTO.Id)).ReturnsAsync(existingTeam);

           
            var result = await _teamController.UpdateTeam(teamDTO);

         
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task DeleteTeam_ReturnsNoContentResult_WhenTeamExists()
        {
         
            var teamId = 1;
            var team = new Team { Id = teamId, Name = "Team 1", IsOpen = true };

            _repositoryWrapperMock.Setup(r => r.Team.GetTeamById(teamId)).ReturnsAsync(team);
            _repositoryWrapperMock.Setup(r => r.Team.DeleteTeam(It.IsAny<Team>()));
            _repositoryWrapperMock.Setup(r => r.Save());

           
            var result = await _teamController.DeleteTeam(teamId);

          
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteTeam_ReturnsNotFoundResult_WhenTeamDoesNotExist()
        {
         
            var teamId = 1;
            Team team = null;

            _repositoryWrapperMock.Setup(r => r.Team.GetTeamById(teamId)).ReturnsAsync(team);

            
            var result = await _teamController.DeleteTeam(teamId);

        
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetAllMembersOfTeam_ReturnsOkResult_WithMembersDTOList_WhenTeamExists()
        {
           
            var teamId = 1;
            var members = new List<Member> {
            new Member { Id = 1, Name = "Member 1" },
            new Member { Id = 2, Name = "Member 2" }
        };

            _repositoryWrapperMock.Setup(r => r.Member.FindAllMembersOfTeam(teamId)).ReturnsAsync(members);

            
            var result = await _teamController.GetAllMembersOfTeam(teamId);

           
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okObjectResult = result as OkObjectResult;
            Assert.IsInstanceOf<List<MemberDTO>>(okObjectResult.Value);
            var membersDTO = okObjectResult.Value as List<MemberDTO>;
            Assert.AreEqual(members.Count, membersDTO.Count);
            for (int i = 0; i < members.Count; i++)
            {
                Assert.AreEqual(members[i].Id, membersDTO[i].Id);
                Assert.AreEqual(members[i].Name, membersDTO[i].Name);
            }
        }

        [Test]
        public async Task GetAllMembersOfTeam_ReturnsNotFoundResult_WhenTeamDoesNotExist()
        {
            // Arrange
            var teamId = 1;
            var _teamRepositoryMock = new Mock<ITeamRepository>();
            var _repositoryWrapperMock = new Mock<IRepositoryWrapper>();

            _repositoryWrapperMock.Setup(rw => rw.Team).Returns(_teamRepositoryMock.Object);
            _teamRepositoryMock.Setup(t => t.GetTeamById(teamId)).ReturnsAsync((Team)null);

            var _teamController = new TeamController(_repositoryWrapperMock.Object);
            
            var result = await _teamController.GetAllMembersOfTeam(teamId);
            Assert.IsTrue(result is ObjectResult);
        }

        [Test]
        public async Task GetTeamById_ReturnsOkResult_WithTeamDTO_WhenTeamExists()
        {
            
            var teamId = 1;
            var team = new Team { Id = teamId, Name = "Team 1", IsOpen = true };

            _repositoryWrapperMock.Setup(r => r.Team.GetTeamById(teamId)).ReturnsAsync(team);

            var result = await _teamController.GetTeamById(teamId);

            
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okObjectResult = result as OkObjectResult;
            Assert.IsInstanceOf<TeamDTO>(okObjectResult.Value);
            var teamDTO = okObjectResult.Value as TeamDTO;
            Assert.AreEqual(team.Id, teamDTO.Id);
            Assert.AreEqual(team.Name, teamDTO.Name);
            Assert.AreEqual(team.IsOpen, teamDTO.IsOpen);
        }

    }

}
