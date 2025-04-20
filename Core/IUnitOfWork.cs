using Core.Entities;
using Core.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        public IGenericRepository<T> GetRepository<T>() where T : BaseEntity;
        public Task<int> CompleteAsync();

    }
}
