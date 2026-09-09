using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookFlow.Core.Entities;

namespace BookFlow.DAL.Configurations;

public class LoanConfiguration
    : IEntityTypeConfiguration<Loan>
{
    public void Configure(
        EntityTypeBuilder<Loan> builder)
    {
        builder.ToTable("Loans");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.Member)
        .WithMany(x => x.Loans)
        .HasForeignKey(x => x.MemberId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.BookCopy)
            .WithMany(x => x.Loans)
            .HasForeignKey(x => x.BookCopyId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
