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

    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        return await FindAll()
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Role> GetRoleById(int id)
    {
        return await FindByCondition(r => r.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public void CreateRole(Role role)
    {
        Create(role);
    }

    public void UpdateRole(Role role)
    {
        Update(role);
    }

    public void DeleteRole(Role role)
    {
        Delete(role);
    }
}