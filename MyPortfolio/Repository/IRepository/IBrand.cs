using MyPortfolio.Models;

namespace MyPortfolio.Repository.IRepository
{
    public interface IBrand:IRepository<Brand>
    {
        void Update(Brand obj);
    }
}
