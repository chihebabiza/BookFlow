using BookFlow.BLL.Exceptions;
using BookFlow.Core.DTOs;
using BookFlow.Core.Enums;
using BookFlow.Core.Interfaces;
using BookFlow.Core.Entities;
using BookFlow.BLL.Helpers;

namespace BookFlow.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(
        IUserRepository repository
        )
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<UserResponseDto> GetByIdAsync(int id)
    {
        ValidationHelper.ValidateId(id);

        var user = await _repository.GetByIdAsync(id);
        if (user is null)
            throw new NotFoundException($"The user with the identifier {id} does not exist");
        return user;
    }

    public async Task<int> CreateAsync(UserCreateDto dto)
    {
        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash),
            Role = dto.Role,
        };
        return await _repository.CreateAsync(user);
    }

    public async Task<bool> UpdateAsync(UserUpdateDto dto, int id)
    {
        ValidationHelper.ValidateId(id);

        var user = await _repository.GetByIdForUpdateAsync(id);
        if (user is null)
            throw new NotFoundException($"The user with the identifier {id} does not exist");

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash);
        user.Role = dto.Role;
        user.IsActive = dto.IsActive;

        return await _repository.UpdateAsync(user);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        ValidationHelper.ValidateId(id);

        var exist = await _repository.IsExistsAsync(id);
        if (!exist)
            throw new NotFoundException($"The user with the identifier {id} does not exist");

        var result = await _repository.DeleteAsync(id);
        return result switch
        {
            DeleteResult.Success => true,

            DeleteResult.HasDependencies => throw new ConflictException(
                "This user cannot be deleted because it has dependencies"),

            DeleteResult.SqlProblem => throw new Exception(
                "An error occurred while deleting the user"),

            _ => throw new Exception(
                "An unexpected error occurred while deleting the user")
        };
    }
}
