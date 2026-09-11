using BookFlow.Core.DTOs;
namespace BookFlow.Core.Interfaces;

public interface ILoanService
{
    Task<IEnumerable<LoanResponseDto>> GetByMemberAsync(int memberId);

    Task<LoanResponseDto> GetByIdAsync(int id);

    Task<int> CreateAsync(LoanCreateDto author);

    Task<bool> UpdateAsync(LoanUpdateDto author, int id);

    Task<bool> DeleteAsync(int id);
}
