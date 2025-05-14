using System.Linq.Expressions;

namespace CQRS_read_infrastructure.Persistence
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(object id);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate = null);
        T Find(object id);
    }
}
