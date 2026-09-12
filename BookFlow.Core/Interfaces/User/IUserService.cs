using BookFlow.Core.DTOs;
namespace BookFlow.Core.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllAsync();

    Task<UserResponseDto> GetByIdAsync(int id);

    Task<int> CreateAsync(UserCreateDto author);

    Task<bool> UpdateAsync(UserUpdateDto author, int id);

    Task<bool> DeleteAsync(int id);

    Task<string> LoginAsync(UserLoginDto dto);
}
