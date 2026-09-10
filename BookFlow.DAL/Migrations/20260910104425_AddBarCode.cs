using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFlow.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddBarCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CopyNumber",
                table: "BookCopies",
                newName: "Barcode");

            migrationBuilder.RenameColumn(
                name: "AcquiredDate",
                table: "BookCopies",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_BookCopies_CopyNumber",
                table: "BookCopies",
                newName: "IX_BookCopies_Barcode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "BookCopies",
                newName: "AcquiredDate");

            migrationBuilder.RenameColumn(
                name: "Barcode",
                table: "BookCopies",
                newName: "CopyNumber");

            migrationBuilder.RenameIndex(
                name: "IX_BookCopies_Barcode",
                table: "BookCopies",
                newName: "IX_BookCopies_CopyNumber");
        }
    }
}
