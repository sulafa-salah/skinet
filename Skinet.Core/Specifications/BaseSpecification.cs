using Skinet.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Specifications
{
    public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
    {
        // Default constructor (no filtering)
        protected BaseSpecification() : this(null) { }

        // Filter condition: where clause
        public Expression<Func<T, bool>>? Criteria => criteria;

        // Sorting: ascending
        public Expression<Func<T, object>>? OrderBy { get; private set; }

        // Sorting: descending
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        // Distinct flag to remove duplicates
        public bool IsDistinct { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        // Helper method to set ascending sort
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            // Throw exception if null passed (fail fast)
            OrderBy = orderByExpression ?? throw new ArgumentNullException(nameof(orderByExpression));
        }

        // Helper method to set descending sort
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression ?? throw new ArgumentNullException(nameof(orderByDescExpression));
        }

        // Helper method to enable DISTINCT in query
        protected void ApplyDistinct()
        {
            IsDistinct = true;
        }
        public IQueryable<T> ApplyCriteria(IQueryable<T> query)
        {
            if (Criteria != null)
            {
                query = query.Where(Criteria);
            }

            return query;
        }
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }

    public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria)
        : BaseSpecification<T>(criteria), ISpecification<T, TResult>
    {
        // Default constructor (no criteria or projection)
        protected BaseSpecification() : this(null) { }
        // Select expression (projection logic)
        public Expression<Func<T, TResult>>? Select { get; private set; }

        // Helper method to set projection (select specific fields to return instead of the full entity)
        // Example: AddSelect(p => new ProductDto { Id = p.Id, Name = p.Name });
        protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
        {
            Select = selectExpression;
        }
    }
}
