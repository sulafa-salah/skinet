using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Persistence
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        // This method builds the query for ISpecification<T>
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
        {
            // Apply filtering if Criteria is provided
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); // x => x.Brand == "React"
            }
            // Apply ascending order if provided
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            // Apply descending order if provided
            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            // Apply distinct to remove duplicates
            if (spec.IsDistinct)
            {
                query = query.Distinct();
            }
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            // Return the final queryable result
            return query;
        }

        public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> query,
            ISpecification<T, TResult> spec)
        {
            // Apply filtering
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); // x => x.Brand == "React"
            }
            // Apply ordering
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            // Apply descending ordering
            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            // Start as the current query, cast to TResult (may still be null)
            var selectQuery = query as IQueryable<TResult>;
            // If a select (projection) is provided, apply it
            if (spec.Select != null)
            {
                selectQuery = query.Select(spec.Select);
            }
            // If distinct is needed, apply it after select
            if (spec.IsDistinct)
            {
                selectQuery = selectQuery?.Distinct();
            }
            if (spec.IsPagingEnabled)
            {
                selectQuery = selectQuery?.Skip(spec.Skip).Take(spec.Take);
            }
            // Return final query (fallback to casting if select is null)
            return selectQuery ?? query.Cast<TResult>();
        }
    }
}
