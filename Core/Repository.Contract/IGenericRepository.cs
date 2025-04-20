using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdSpecAsync(ISpecification<T> spec);
        Task<IEnumerable<T>> GetAllSpecAsync(ISpecification<T> spec);
        Task<int> CountSpecAsync(ISpecification<T> spec);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
