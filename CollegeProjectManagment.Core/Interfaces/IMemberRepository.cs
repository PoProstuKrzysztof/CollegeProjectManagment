using CollegeProjectManagment.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Interfaces;

public interface IMemberRepository : IRepositoryBase<Member>
{
    // tutaj usuwamy wszystkich memberów powiązanych z rolą jak jest usuwana
    IEnumerable<Member> MembersByRole(int roleId);

    Task<IEnumerable<Member>> GetAllMembers();

    Task<Member> GetMemberById(int? id);

    void CreateMember(Member member);

    void UpdateMember(Member member);

    void DeleteMember(Member member);
}