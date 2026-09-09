using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
using BookFlow.Core.Enums;

namespace BookFlow.Core.Interfaces;

public interface ILoanRepository
{
    Task<IEnumerable<LoanResponseDto>> GetAllAsync();

    Task<LoanResponseDto?> GetByIdAsync(int id);

    Task<Loan?> GetByIdForUpdateAsync(int id);

    Task<int> CreateAsync(Loan book);

    Task<bool> UpdateAsync(Loan book);

    Task<DeleteResult> DeleteAsync(int id);

    Task<bool> IsExistsAsync(int id);
}
