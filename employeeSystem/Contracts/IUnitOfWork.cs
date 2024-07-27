using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Irepository<Employee> Employees { get; }
        Task<int> SaveChangesAsync();
    }
}
