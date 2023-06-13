using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.DTO;
using CollegeProjectManagment.Core.Enums;
using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Infrastructure.Repositories;

public class MemberRepository : RepositoryBase<Member>, IMemberRepository
{
    public MemberRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public IEnumerable<Member> MembersByRole(int roleId)
    {
        return FindByCondition(r => r.RoleId.Equals(roleId)).ToList();
    }

    public async Task<IEnumerable<Member>> GetAllMembers()
    {
        return await FindAll()
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<Member> GetMemberById(int? id)
    {
        return await FindByCondition(x => x.Id.Equals(id)).FirstOrDefaultAsync(); ;
    }

    public void CreateMember(Member member)
    {
        Create(member);
    }

    public void UpdateMember(Member member)
    {
        Update(member);
    }

    public void DeleteMember(Member member)
    {
        Delete(member);
    }

    /// <summary>
    /// Increasing prestige points every time when project is moving to the next state
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    public async Task IncreasePrestigePointsForTeamMembers(int teamId)
    {
        var members = await FindAll()
            .Where(x => x.TeamId.Equals(teamId))
            .ToListAsync();

        foreach (var member in members)
        {
            member.PrestigePoints += 10;
            Update(member);
        }
    }

    public async Task<List<Member>> FindAllMembersOfTeam(int? teamId)
    {
        var members = await FindAll()
            .Where(x => x.TeamId.Equals(teamId))
            .ToListAsync();

        return members;
    }

    public async Task<int> CountMembersOfTeam(int? teamId)
    {
        var members = await FindAll()
            .Where(x => x.TeamId.Equals(teamId))
            .ToListAsync();

        return members.Count();
    }

    public void ManagePoints(List<Member> members, string command, int multiplier)
    {
        if (command is null)
        {
            command = "back";
        }

        if (command.ToLower().Equals("next"))
        {

            foreach (var m in members)
            {
                m.AddPoints(multiplier);
                Update(m);
            }
        }
        else
        {
            foreach (var m in members)
            {
                m.SubtractPoints(multiplier);
                Update(m);
            }
        }
    }

    public async Task<IEnumerable<Member>> GetAllMembersByTechnology(string tech)
    {
        var parsedEnum = Enum.Parse<ProgrammingLanguages>(tech);

        var members = await FindAll()
             .OrderBy(x => x.Id)
             .ToListAsync();

        var membersWithDesiredTech = members.Where(x => x.KnownTechnologies.Contains(parsedEnum))
            .ToList();

        return membersWithDesiredTech;
    }
}