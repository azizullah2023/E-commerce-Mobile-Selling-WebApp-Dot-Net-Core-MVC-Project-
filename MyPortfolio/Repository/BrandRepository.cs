using MyPortfolio.Repository.IRepository;
using MyPortfolio.Models;
using MyPortfolio.Data;

namespace MyPortfolio.Repository
{
    public class BrandRepository : Repository<Brand>, IBrand
    {
        private readonly ApplicationDbContext _context;
        public BrandRepository(ApplicationDbContext context) :base(context)
        {
            _context = context;
        }
        public void Update(Brand obj)
        {
            _context.Update(obj);
            _context.SaveChanges();
        }
    }
}
