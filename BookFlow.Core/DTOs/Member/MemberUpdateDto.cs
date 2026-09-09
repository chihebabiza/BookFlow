namespace BookFlow.Core.DTOs;

public class MemberUpdateDto
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public bool IsActive { get; set; } = true;
}
