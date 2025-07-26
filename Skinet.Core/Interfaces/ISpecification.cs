using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Interfaces
{
    public interface ISpecification<T>
    {
        // The filter condition, like: x => x.Brand == "Nike"
        Expression<Func<T, bool>>? Criteria { get; }
        // Sort the results in ascending order: x => x.Name
        Expression<Func<T, object>>? OrderBy { get; }
        // Sort the results in descending order: x => x.Price
        Expression<Func<T, object>>? OrderByDescending { get; }
        // Remove duplicate records if true
        bool IsDistinct { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
        IQueryable<T> ApplyCriteria(IQueryable<T> query);
    }

    public interface ISpecification<T, TResult> : ISpecification<T>
    {
        // Project (select) only specific fields from T into TResult
        // e.g. x => new ProductDto { Id = x.Id, Name = x.Name }
        Expression<Func<T, TResult>>? Select { get; }
    }
}
