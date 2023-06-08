using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.DTO;
using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Infrastructure.Repositories;

public class TeamRepository : RepositoryBase<Team>, ITeamRepository
{
    public TeamRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Team>> GetAllTeams()
    {
        return await FindAll()
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<Team> GetTeamById(int id)
    {
        return await FindByCondition(x => x.Id.Equals(id))
            .FirstOrDefaultAsync();
    }

    public void UpdateTeam(Team team)
    {
        Update(team);
    }

    public async void CreateTeam(Team team)
    {
        Create(team);
    }

    public void DeleteTeam(Team team)
    {
        Delete(team);
    }
}