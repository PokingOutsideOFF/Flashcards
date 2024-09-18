using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flashcards.Migrations
{
    public partial class UniqueConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StackName",
                table: "Stack",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "Flashcard",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Stack_StackName",
                table: "Stack",
                column: "StackName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flashcard_Question",
                table: "Flashcard",
                column: "Question",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stack_StackName",
                table: "Stack");

            migrationBuilder.DropIndex(
                name: "IX_Flashcard_Question",
                table: "Flashcard");

            migrationBuilder.AlterColumn<string>(
                name: "StackName",
                table: "Stack",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "Flashcard",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
