using BookFlow.BLL.Exceptions;
using BookFlow.BLL.Helpers;
using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
using BookFlow.Core.Enums;
using BookFlow.Core.Interfaces;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

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

    public async Task<string> LoginAsync(UserLoginDto user)
    {
        var loggedInUser = await _repository.GetByEmailAsync(user.Email);

        if (loggedInUser is null)
            throw new UnauthorizedAccessException("Invalid credentials");

        if (!loggedInUser.IsActive)
            throw new UnauthorizedAccessException("This account is inactive");

        bool isValidPassword =
            BCrypt.Net.BCrypt.Verify(
                user.PasswordHash,
                loggedInUser.PasswordHash);

        if (!isValidPassword)
            throw new UnauthorizedAccessException("Invalid credentials");

        var claims = new[]
        {
        new Claim(
            ClaimTypes.NameIdentifier,
            loggedInUser.Id.ToString()),

        new Claim(
            ClaimTypes.Email,
            loggedInUser.Email),

        new Claim(
            ClaimTypes.Role,
            loggedInUser.Role.ToString())
    };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                "THIS_IS_A_VERY_SECRET_KEY_123456"));

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "StudentApi",
            audience: "StudentApiUsers",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
