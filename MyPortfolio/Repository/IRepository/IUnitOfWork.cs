namespace MyPortfolio.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IBrand Brandy { get; }
        IMobileSelling mobileSelling { get; }
        IStriper striper { get; }
    }
}
