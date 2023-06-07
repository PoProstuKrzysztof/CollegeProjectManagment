using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.DTO;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Mapper;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName, EnumMappingIgnoreCase = true)]
public partial class Mapper
{
    [MapperIgnoreSource(nameof(Project.AssignedTeamId))]
    [MapperIgnoreSource(nameof(Project.LeaderId))]
    public partial ProjectDTO MapProjectToProjectDTO(Project project);

    public partial TeamDTO MapTeamToTeamDTO(Team team);

    public partial MemberDTO MapMemberToMemberDTO(Member member);

    public partial RoleDTO MapRoleToRoleDTO(Role role);

    public partial Role MapRoletDtoToProject(RoleDTO project);
}