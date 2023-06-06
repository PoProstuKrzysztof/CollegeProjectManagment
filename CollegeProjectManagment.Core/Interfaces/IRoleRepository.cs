using CollegeProjectManagment.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Interfaces;

public interface IRoleRepository : IRepositoryBase<Role>
{
    Task<Role> GetRoleById(int id);

    Task<Role> GetRoleWithMembers(int id);
    void CreateRole(Role role);
}