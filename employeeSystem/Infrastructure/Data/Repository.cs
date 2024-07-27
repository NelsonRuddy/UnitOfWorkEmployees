using Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class Repository<T> : Irepository<T> where T : class
    {
        public readonly EmployeeDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(EmployeeDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();

        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity); 
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Entry(entity).State = EntityState.Deleted;
           
            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Entry(entity).State = EntityState.Modified;
            _dbSet.Update(entity);
            await Task.CompletedTask;

          

        }
    }
}
