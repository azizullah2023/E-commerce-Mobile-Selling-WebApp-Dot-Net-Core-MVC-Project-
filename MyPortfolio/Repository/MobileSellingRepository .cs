using MyPortfolio.Repository.IRepository;
using MyPortfolio.Models;
using MyPortfolio.Data;

namespace MyPortfolio.Repository
{
    public class MobileSellingRepository : Repository<FormMobileSellIt>, IMobileSelling
    {
        private readonly ApplicationDbContext _context;
        public MobileSellingRepository(ApplicationDbContext context) :base(context)
        {
            _context = context;
        }
        public void Update(FormMobileSellIt obj)
        {

            _context.Update(obj);
            _context.SaveChanges();
        }
    }
}
