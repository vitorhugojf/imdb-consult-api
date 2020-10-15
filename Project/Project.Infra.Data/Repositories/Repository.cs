using Microsoft.EntityFrameworkCore;
using Project.Domain.Shared.Entities;
using Project.Domain.Shared.Interfaces.Repositories;
using Project.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Infra.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly DataContext DbContext;

        public Repository(DataContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T> Create(T objeto)
        {
            await DbContext.Set<T>().AddAsync(objeto);
            return objeto;
        }

        public T Update(T objeto)
        {
            DbContext.Entry(objeto).State = EntityState.Modified;
            return objeto;
        }

        public void Inactivate(T entity)
        {
            DbContext.Entry(entity).Property(x => x.Active).IsModified = true;
        }

        public async Task<T> GetById(int id)
        {
            return await DbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<T>> GetAll()
        {
            return await DbContext.Set<T>().ToListAsync();
        }
    }
}
