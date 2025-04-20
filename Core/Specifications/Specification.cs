using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class Specification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> criteria { get; set; }
        public List<Expression<Func<T, object>>> includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public bool IsPagination { get; set; }

        public Specification()
        {

        }
        public Specification(Expression<Func<T, bool>> cretira)
        {
            criteria = cretira;

        }
        protected void ApplyPagination(int skip, int take)
        {
            IsPagination = true;
            this.skip = skip;
            this.take = take;
        }
    }
}
