using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SequencedImages_Articles_ArticleId",
                table: "SequencedImages");

            migrationBuilder.AddForeignKey(
                name: "FK_SequencedImages_Articles_ArticleId",
                table: "SequencedImages",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SequencedImages_Articles_ArticleId",
                table: "SequencedImages");

            migrationBuilder.AddForeignKey(
                name: "FK_SequencedImages_Articles_ArticleId",
                table: "SequencedImages",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
