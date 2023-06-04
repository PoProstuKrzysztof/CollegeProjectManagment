using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Infrastructure.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
    private RepositoryContext _context;
    private IMemberRepository _member;
    private IRoleRepository _role;
    private ITeamRepository _team;
    private IProjectRepository _project;

    public RepositoryWrapper(RepositoryContext context)
    {
        _context = context;
    }

    public IMemberRepository Member
    {
        get
        {
            if (_member == null)
            {
                _member = new MemberRepository(_context);
            }

            return _member;
        }
    }

    public IRoleRepository Role
    {
        get
        {
            if (_role == null)
            {
                _role = new RoleRepository(_context);
            }

            return _role;
        }
    }

    public ITeamRepository Team
    {
        get
        {
            if (_team == null)
            {
                _team = new TeamRepository(_context);
            }

            return _team;
        }
    }

    public IProjectRepository Project
    {
        get
        {
            if (_project == null)
            {
                _project = new ProjectRepository(_context);
            }

            return _project;
        }
    }

    public async void Save()
    {
        await _context.SaveChangesAsync();
    }
}