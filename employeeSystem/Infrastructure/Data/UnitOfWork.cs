using Contracts;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeeDbContext _context;

        private Irepository<Employee> _employees;

        public UnitOfWork(EmployeeDbContext context) { 

            _context = context ?? throw new ArgumentNullException(nameof(context));
        
        }

        public Irepository<Employee> Employees => _employees ??= new Repository<Employee>(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
