using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
using BookFlow.Core.Enums;

namespace BookFlow.Core.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserResponseDto>> GetAllAsync();

    Task<UserResponseDto?> GetByIdAsync(int id);

    Task<User?> GetByIdForUpdateAsync(int id);

    Task<int> CreateAsync(User book);

    Task<bool> UpdateAsync(User book);

    Task<DeleteResult> DeleteAsync(int id);

    Task<bool> IsExistsAsync(int id);
}
