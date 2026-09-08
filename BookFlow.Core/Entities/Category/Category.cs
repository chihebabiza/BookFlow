namespace BookFlow.Core.Entities;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
