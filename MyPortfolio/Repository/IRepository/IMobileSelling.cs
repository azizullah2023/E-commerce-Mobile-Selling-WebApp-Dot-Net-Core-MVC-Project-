using MyPortfolio.Models;

namespace MyPortfolio.Repository.IRepository
{
    public interface IMobileSelling : IRepository<FormMobileSellIt>
    {
        void Update(FormMobileSellIt obj);
    }
}
