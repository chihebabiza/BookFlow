using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
using BookFlow.Core.Enums;

namespace BookFlow.Core.Interfaces;

public interface ILoanRepository
{
    Task<IEnumerable<LoanResponseDto>> GetByMemberAsync(int memberId);

    Task<LoanResponseDto?> GetByIdAsync(int id);

    Task<Loan?> GetByIdForUpdateAsync(int id);

    void Create(Loan book);

    void Update(Loan book);

    Task<DeleteResult> DeleteAsync(int id);

    Task<bool> IsExistsAsync(int id);
}
