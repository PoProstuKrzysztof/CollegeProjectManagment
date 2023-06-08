using CollegeProjectManagment.Core.Domain.Entities;
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
}