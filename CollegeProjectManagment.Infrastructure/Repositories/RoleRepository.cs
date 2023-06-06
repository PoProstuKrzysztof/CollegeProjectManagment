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

public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
    public RoleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateRole(Role role)
    {
        Create(role);
    }

    public IQueryable<Role> GetAll()
    {
        return FindAll().OrderBy(r => r.Name);
    }

    public async Task<Role> GetRoleById(int id)
    {
        return await FindByCondition(r => r.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public async Task<Role> GetRoleWithMembers(int id)
    {
        return await FindByCondition(r => r.Id.Equals(id))
            .Include(m => m.Members)
            .FirstOrDefaultAsync();
    }
}