using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AbstractRepositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(); // Task, bu metodun Asenkron kullanılmasına olanak sağlar
        Task<IEnumerable<T>> GetAllDeletedAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task RemoveAsync(int id);
    }
}
