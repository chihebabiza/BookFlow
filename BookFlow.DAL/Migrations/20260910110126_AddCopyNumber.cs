using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFlow.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCopyNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookCopies_Barcode",
                table: "BookCopies");

            migrationBuilder.DropIndex(
                name: "IX_BookCopies_BookId",
                table: "BookCopies");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "BookCopies");

            migrationBuilder.AddColumn<int>(
                name: "CopyNumber",
                table: "BookCopies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_BookId_CopyNumber",
                table: "BookCopies",
                columns: new[] { "BookId", "CopyNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookCopies_BookId_CopyNumber",
                table: "BookCopies");

            migrationBuilder.DropColumn(
                name: "CopyNumber",
                table: "BookCopies");

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "BookCopies",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_Barcode",
                table: "BookCopies",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_BookId",
                table: "BookCopies",
                column: "BookId");
        }
    }
}
