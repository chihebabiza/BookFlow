namespace BookFlow.Core.Entities;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
