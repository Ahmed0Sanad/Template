using Core.Entities;
using Core.Repository.Contract;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _appDbContext;
        public GenericRepository(AppDbContext context)
        {
            _appDbContext = context;

        }
        public async Task AddAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);

        }

        public Task<int> CountSpecAsync(ISpecification<T> spec)
        {
            return BaseQuary(spec).CountAsync();
        }

        public void Delete(T entity)
        {
            _appDbContext.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {

            return await _appDbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllSpecAsync(ISpecification<T> spec)
        {
            return await BaseQuary(spec).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdSpecAsync(ISpecification<T> spec)
        {
            return await BaseQuary(spec).FirstOrDefaultAsync();
        }

        public void Update(T entity)
        {
            _appDbContext.Update(entity);
        }
        private IQueryable<T> BaseQuary(ISpecification<T> spec)
        {
            return SpecificationProvider<T>.GetQuary(_appDbContext.Set<T>(), spec);
        }
    }
}
