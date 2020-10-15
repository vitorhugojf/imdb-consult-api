using Project.Domain.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Domain.Shared.Interfaces.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> Create(T obj);
        T Update(T obj);
        Task<T> GetById(int id);
        Task<IList<T>> GetAll();
        void Inactivate(T entity);
    }
}
