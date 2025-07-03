using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public static class SpecificationEvaluator
    {

        public static IQueryable<T> CreateQuery<T>(IQueryable<T> inputQuery , ISpecifications<T> specifications) where T : class
        {
            var query = inputQuery;

            if (specifications.Criteria is not null)
                query = query.Where(specifications.Criteria);

            foreach (var include in specifications.IncludeExpressions)
            {
                query.Include(include);
            }

            return query;
        }
    }
}
