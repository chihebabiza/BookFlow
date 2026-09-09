namespace BookFlow.Core.DTOs;

public class MemberResponseDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; } = true;
}
