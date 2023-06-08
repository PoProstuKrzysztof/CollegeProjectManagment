using CollegeProjectManagment.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Interfaces;

public interface IRoleRepository : IRepositoryBase<Role>
{
    //BASIC CRUD
    Task<IEnumerable<Role>> GetAllRoles();

    Task<Role> GetRoleById(int id);

    void CreateRole(Role role);

    void UpdateRole(Role role);

    void DeleteRole(Role role);
}