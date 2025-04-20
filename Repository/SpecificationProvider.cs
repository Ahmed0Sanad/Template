using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class SpecificationProvider<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuary(IQueryable<T> intputQuary, ISpecification<T> spec)
        {
            var quary = intputQuary;
            //where
            if (spec.criteria != null)
            {
                quary = quary.Where(spec.criteria);

            }

            //orderby
            if (spec.OrderBy != null)
            {
                quary = quary.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDesc != null)
            {
                quary = quary.OrderByDescending(spec.OrderByDesc);
            }

            //skip-take
            if (spec.IsPagination)
            {
                quary = quary.Skip(spec.skip).Take(spec.take);
            }

            quary = spec.includes.Aggregate(quary, (q, s) => q.Include(s));
            return quary;
        }
    }
}
