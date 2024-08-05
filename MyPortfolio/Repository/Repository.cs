using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace MyPortfolio.Repository
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _Set;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _Set = context.Set<TEntity>();
            _context.FormMobileSellIts.Include(u=>u.Brand);
        }

        public void Add(TEntity entity)
        {
            _Set.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll(string? IncludeProperties = null)
        {
            IQueryable<TEntity> query = _Set.AsQueryable();
            if (IncludeProperties != null)
            {
                foreach (var Includeprop in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(Includeprop);
                }
            }

            return query.ToList();

        }


        public TEntity GetBYId( Expression<Func<TEntity, bool>> filter, string? IncludeProperties = null)
        {
            IQueryable<TEntity> query = _Set;
            if (IncludeProperties != null)
            {
                foreach (var Includeprop in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(Includeprop);
                }
            }
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Remove(TEntity entity)
        {
            _Set.Remove(entity);
            _context.SaveChanges();
        }
    }
}
