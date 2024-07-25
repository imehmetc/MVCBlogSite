using DAL.AbstractRepositories;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ConcreteRepositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _entities;
        public Repository(AppDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync() // NOT: asenkron oldugundan metoda "async" eklenir ve kullanıldığı yerin başına "await" eklenir.
        {
            return await _entities.Where(x => !x.IsDeleted).ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllDeletedAsync()
        {
            return await _entities.Where(x => x.IsDeleted).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entity)
        {
            await _entities.AddRangeAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.DeletedDate = DateTime.Now;
                _entities.Update(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task RemoveAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity != null)
            {
                _entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Id == id); // Admin göreceğinden IsDeleted filtrelemesi yapmadık.
        }

        public async Task UpdateAsync(T entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
