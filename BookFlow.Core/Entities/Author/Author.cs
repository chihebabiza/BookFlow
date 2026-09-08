namespace BookFlow.Core.Entities;

public class Author
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public int CountryId { get; set; }

    public Country Country { get; set; } = null!;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
