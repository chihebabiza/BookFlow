using BookFlow.Core.Interfaces;
using BookFlow.DAL.Context;

namespace BookFlow.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ILoanRepository Loans { get; }
        public IBookCopyRepository BookCopies { get; }
        public UnitOfWork(
            AppDbContext context,
            ILoanRepository loanRepository,
            IBookCopyRepository bookCopyRepository)
        {
            _context = context;
            Loans = loanRepository;
            BookCopies = bookCopyRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
