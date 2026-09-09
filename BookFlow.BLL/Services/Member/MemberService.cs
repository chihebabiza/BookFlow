using BookFlow.BLL.Exceptions;
using BookFlow.Core.DTOs;
using BookFlow.Core.Enums;
using BookFlow.Core.Interfaces;
using BookFlow.Core.Entities;
using BookFlow.BLL.Helpers;

namespace BookFlow.BLL.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _repository;

    public MemberService(
        IMemberRepository repository
        )
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MemberResponseDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<MemberResponseDto> GetByIdAsync(int id)
    {
        ValidationHelper.ValidateId(id);

        var member = await _repository.GetByIdAsync(id);
        if ( member is null)
            throw new NotFoundException($"The member with the identifier {id} does not exist");

        return member;
    }

    public async Task<int> CreateAsync(MemberCreateDto dto)
    {
        var member = new Member
        {
            CreatedAt = DateTime.UtcNow
        };
        return await _repository.CreateAsync(member);
    }

    public async Task<bool> UpdateAsync(MemberUpdateDto dto, int id)
    {
        ValidationHelper.ValidateId(id);

        var member = await _repository.GetByIdForUpdateAsync(id);
        if ( member is null)
            throw new NotFoundException($"The member with the identifier {id} does not exist");

        return await _repository.UpdateAsync(member);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        ValidationHelper.ValidateId(id);

        var exist = await _repository.IsExistsAsync(id);
        if (!exist)
            throw new NotFoundException($"The  with the identifier {id} does not exist");

        var result = await _repository.DeleteAsync(id);
        return result switch
        {
            DeleteResult.Success => true,

            DeleteResult.HasDependencies => throw new ConflictException(
                "This  cannot be deleted because it has dependencies"),

            DeleteResult.SqlProblem => throw new Exception(
                "An error occurred while deleting the "),

            _ => throw new Exception(
                "An unexpected error occurred while deleting the ")
        };
    }
}
