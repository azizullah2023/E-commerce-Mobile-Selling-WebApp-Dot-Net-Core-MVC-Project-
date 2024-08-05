using MyPortfolio.Data;
using MyPortfolio.Repository.IRepository;

namespace MyPortfolio.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public  IMobileSelling mobileSelling { get; private set; }
        public  IStriper striper { get; private set; }
        public IBrand Brandy { get; private set; }
         
        public UnitOfWork(ApplicationDbContext context) 
        {
            _context = context;
            Brandy = new BrandRepository(_context);
            mobileSelling = new MobileSellingRepository(_context);
            striper = new StriperRepository(_context);
        }
        
    }
}
