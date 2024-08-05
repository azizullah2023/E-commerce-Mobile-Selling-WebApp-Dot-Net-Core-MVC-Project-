using MyPortfolio.Repository.IRepository;
using MyPortfolio.Models;
using MyPortfolio.Data;
using System.Linq.Expressions;

namespace MyPortfolio.Repository
{
    public class StriperRepository : Repository<StripeModel>, IStriper
    {
        private readonly ApplicationDbContext _context;
        public StriperRepository(ApplicationDbContext context) :base(context)
        {
            _context = context;
        }

        

        public void Update(StripeModel obj)
        {
            _context.Update(obj);
            _context.SaveChanges();
        }

        public void UpdateStripeRecord(int id, string sessionid, string paymentintentid)
        {
            var striperecord = new StripeModel { 
            
            SessionI=sessionid,
            PaymentIntentId=paymentintentid,
            PaymentDate=DateTime.Now

            };
            _context.Stripes.Add(striperecord);
            _context.SaveChanges();
        }
        public void UpdationStripeRecord(int id, string sessionid, string paymentintentid)
        {

            var row = _context.Stripes.FirstOrDefault(u=>u.Id==id);
               row.SessionI=sessionid;
            row.PaymentIntentId = paymentintentid;
            row.PaymentDate = DateTime.Now;


           
            _context.Stripes.Update(row);
            _context.SaveChanges();
        }
    }
}
