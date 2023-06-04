using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Infrastructure.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext RepositoryContext { get; set; }

    public RepositoryBase(RepositoryContext repositoryContext)
    {
        RepositoryContext = repositoryContext;
    }

    public IQueryable<T> FindAll() => RepositoryContext.Set<T>().AsNoTracking();

    // we add AsNoTracking to keep high preformance with loading entities
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
        RepositoryContext.Set<T>().Where(expression);

    public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);

    public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);

    public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
}