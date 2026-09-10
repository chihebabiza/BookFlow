using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookFlow.Core.Entities;

namespace BookFlow.DAL.Configurations;

public class BookCopyConfiguration
    : IEntityTypeConfiguration<BookCopy>
{
    public void Configure(
        EntityTypeBuilder<BookCopy> builder)
    {
        builder.ToTable("BookCopies");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(x => new { x.BookId, x.CopyNumber })
            .IsUnique();

        // Relationships
        builder.HasOne(x => x.Book)
            .WithMany(b => b.Copies)
            .HasForeignKey(x => x.BookId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
