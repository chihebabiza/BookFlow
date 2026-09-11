namespace BookFlow.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ILoanRepository Loans { get; }
        IBookCopyRepository BookCopies { get; }
        Task<int> CompleteAsync();
    }
}
