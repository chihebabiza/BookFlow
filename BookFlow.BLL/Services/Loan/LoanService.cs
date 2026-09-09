using BookFlow.BLL.Exceptions;
using BookFlow.Core.DTOs;
using BookFlow.Core.Enums;
using BookFlow.Core.Interfaces;
using BookFlow.Core.Entities;
using BookFlow.BLL.Helpers;

namespace BookFlow.BLL.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _repository;

    public LoanService(
        ILoanRepository repository
        )
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LoanResponseDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<LoanResponseDto> GetByIdAsync(int id)
    {
        ValidationHelper.ValidateId(id);

        var loan = await _repository.GetByIdAsync(id);
        if (loan is null)
            throw new NotFoundException($"The loan with the identifier {id} does not exist");
        return loan;
    }

    public async Task<int> CreateAsync(LoanCreateDto dto)
    {
        var loan = new Loan
        {
            //CreatedAt = DateTime.UtcNow
        };
        return await _repository.CreateAsync(loan);
    }

    public async Task<bool> UpdateAsync(LoanUpdateDto dto, int id)
    {
        ValidationHelper.ValidateId(id);

        var loan = await _repository.GetByIdForUpdateAsync(id);
        if (loan is null)
            throw new NotFoundException($"The loan with the identifier {id} does not exist");

        return await _repository.UpdateAsync(loan);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        ValidationHelper.ValidateId(id);

        var exist = await _repository.IsExistsAsync(id);
        if (!exist)
            throw new NotFoundException($"The loan with the identifier {id} does not exist");

        var result = await _repository.DeleteAsync(id);
        return result switch
        {
            DeleteResult.Success => true,

            DeleteResult.HasDependencies => throw new ConflictException(
                "This loan cannot be deleted because it has dependencies"),

            DeleteResult.SqlProblem => throw new Exception(
                "An error occurred while deleting the loan"),

            _ => throw new Exception(
                "An unexpected error occurred while deleting the loan")
        };
    }
}
