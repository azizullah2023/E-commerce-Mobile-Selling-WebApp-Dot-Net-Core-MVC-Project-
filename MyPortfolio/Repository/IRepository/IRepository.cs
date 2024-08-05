using MyPortfolio.Models;
using System.Linq.Expressions;

namespace MyPortfolio.Repository.IRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetBYId (Expression<Func<TEntity,bool>> flter, string? IncludeProperties = null);
        IEnumerable<TEntity> GetAll(string? IncludeProperties = null);
        void Add(TEntity entity);

        void Remove(TEntity entity);
        //TEntity GetById(int id);
        //IEnumerable<TEntity> GetAll();
        //IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        //void Add(TEntity entity);
        //void AddRange(IEnumerable<TEntity> entities);
        //void Remove(TEntity entity);
        //void RemoveRange(IEnumerable<TEntity> entities);
    }
}
