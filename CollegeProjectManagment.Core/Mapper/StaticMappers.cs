using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.DTO;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Mapper;

[Mapper]
public static partial class StaticMappers
{
    public static partial IEnumerable<RoleDTO> RolesToRoleDTO(this IEnumerable<Role> roleDTO);
}