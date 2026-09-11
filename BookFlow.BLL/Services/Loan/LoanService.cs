using BookFlow.BLL.Exceptions;
using BookFlow.Core.DTOs;
using BookFlow.Core.Enums;
using BookFlow.Core.Interfaces;
using BookFlow.Core.Entities;
using BookFlow.BLL.Helpers;

namespace BookFlow.BLL.Services;

public class LoanService : ILoanService
{
    private readonly IUnitOfWork _unitOfWork;
    private IMemberRepository _memberRepository;

    public LoanService(IUnitOfWork unitOfWork, IMemberRepository memberRepository)
    {
        _unitOfWork = unitOfWork;
        _memberRepository = memberRepository;
    }

    public async Task<IEnumerable<LoanResponseDto>> GetByMemberAsync(int memberId)
    {
        ValidationHelper.ValidateId(memberId);

        if (!await _memberRepository.IsExistsAsync(memberId))
            throw new NotFoundException($"The member with the identifier {memberId} does not exist");

        return await _unitOfWork.Loans.GetByMemberAsync(memberId);
    }

    public async Task<LoanResponseDto> GetByIdAsync(int id)
    {
        ValidationHelper.ValidateId(id);

        var loan = await _unitOfWork.Loans.GetByIdAsync(id);
        if (loan is null)
            throw new NotFoundException($"The loan with the identifier {id} does not exist");
        return loan;
    }

    public async Task<int> CreateAsync(LoanCreateDto dto)
    {
        var memberExists = await _memberRepository.IsExistsAsync(dto.MemberId);

        if (!memberExists)
            throw new NotFoundException(
                $"The member with the identifier {dto.MemberId} does not exist");

        var memberAcative = await _memberRepository.IsActiveAsync(dto.MemberId);

        if (!memberAcative)
            throw new BadRequestException("The member is not active.");

        var bookCopy = await _unitOfWork.BookCopies.GetByIdForUpdateAsync(dto.BookCopyId);

        if (bookCopy == null)
            throw new NotFoundException(
                $"The book copy with the identifier {dto.BookCopyId} does not exist");

        if (bookCopy.Status != BookCopyStatus.Available)
            throw new BadRequestException("The book copy is not available.");

        var loan = new Loan
        {
            MemberId = dto.MemberId,
            BookCopyId = dto.BookCopyId,
            BorrowedDate = dto.BorrowedDate,
            DueDate = dto.BorrowedDate.AddDays(dto.Period)
        };

        bookCopy.Status = BookCopyStatus.Borrowed;

        _unitOfWork.Loans.Create(loan);
        _unitOfWork.BookCopies.Update(bookCopy);

        await _unitOfWork.CompleteAsync();

        return loan.Id;
    }

    public async Task<bool> UpdateAsync(LoanUpdateDto dto, int id)
    {
        ValidationHelper.ValidateId(id);

        var loan = await _unitOfWork.Loans.GetByIdForUpdateAsync(id);

        if (loan is null)
            throw new NotFoundException(
                $"The loan with the identifier {id} does not exist");

        if (loan.Status == LoanStatus.Returned)
            throw new BadRequestException("This loan has already been returned.");

        var bookCopy = await _unitOfWork.BookCopies
            .GetByIdForUpdateAsync(loan.BookCopyId);

        if (bookCopy is null)
            throw new NotFoundException(
                $"The book copy with the identifier {loan.BookCopyId} does not exist");

        loan.ReturnedDate = dto.ReturnedDate;
        loan.Status = LoanStatus.Returned;
        bookCopy.Status = BookCopyStatus.Available;

        _unitOfWork.Loans.Update(loan);
        _unitOfWork.BookCopies.Update(bookCopy);

        await _unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        ValidationHelper.ValidateId(id);

        var exist = await _unitOfWork.Loans.IsExistsAsync(id);
        if (!exist)
            throw new NotFoundException($"The loan with the identifier {id} does not exist");

        var result = await _unitOfWork.Loans.DeleteAsync(id);
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
