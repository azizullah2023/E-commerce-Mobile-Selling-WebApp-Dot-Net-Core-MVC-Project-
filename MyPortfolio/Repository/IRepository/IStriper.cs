using MyPortfolio.Models;

namespace MyPortfolio.Repository.IRepository
{
    public interface IStriper:IRepository<StripeModel>
    {
        void Update(StripeModel obj);
        void UpdateStripeRecord(int id,string sessionid, string paymentintentid);
        public void UpdationStripeRecord(int id, string sessionid, string paymentintentid);
    }
}
