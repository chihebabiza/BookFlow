using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
using BookFlow.Core.Enums;

namespace BookFlow.Core.Interfaces;

public interface IMemberRepository
{
    Task<IEnumerable<MemberResponseDto>> GetAllAsync();

    Task<MemberResponseDto?> GetByIdAsync(int id);

    Task<Member?> GetByIdForUpdateAsync(int id);

    Task<int> CreateAsync(Member book);

    Task<bool> UpdateAsync(Member book);

    Task<DeleteResult> DeleteAsync(int id);

    Task<bool> IsExistsAsync(int id);

    Task<bool> IsActiveAsync(int id);
}
