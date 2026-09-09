using BookFlow.Core.DTOs;
namespace BookFlow.Core.Interfaces;

public interface IMemberService
{
    Task<IEnumerable<MemberResponseDto>> GetAllAsync();

    Task<MemberResponseDto> GetByIdAsync(int id);

    Task<int> CreateAsync(MemberCreateDto author);

    Task<bool> UpdateAsync(MemberUpdateDto author, int id);

    Task<bool> DeleteAsync(int id);
}
