using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Interfaces;

public interface ITeamRepository : IRepositoryBase<Team>
{    //BASIC CRUD
    Task<IEnumerable<Team>> GetAllTeams();

    Task<Team> GetTeamById(int id);

    void CreateTeam(Team team);

    void UpdateTeam(Team team);

    void DeleteTeam(Team team);

    // INDIVIDUAL OPERATIONS
}