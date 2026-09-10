namespace BookFlow.Core.DTOs;

public class AuthorResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public CountryResponseDto Country{ get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
