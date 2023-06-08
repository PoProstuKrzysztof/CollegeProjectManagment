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
    //Mapowanie poszczególnych encji na DTO i odwrotnie

    //Projects
    public partial ProjectDTO MapProjectToProjectDTO(Project project);

    public partial Project MapyProjectDtoToProject(ProjectDTO projectDTO);

    //Teams

    public partial TeamDTO MapTeamToTeamDTO(Team team);

    public partial Team MapTeamDtoToTeam(TeamDTO teamDTO);

    //Members
    public partial MemberDTO MapMemberToMemberDTO(Member member);

    public partial Member MapMemberDtoToMember(MemberDTO memberDTO);

    //Roles
    public partial RoleDTO MapRoleToRoleDTO(Role role);

    public partial Role MapRoletDtoToProject(RoleDTO roleDTO);
}