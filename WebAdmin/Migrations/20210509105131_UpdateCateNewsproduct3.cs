using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAdmin.Migrations
{
    public partial class UpdateCateNewsproduct3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TagsReference",
                table: "Article",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CategoryReference",
                table: "Article",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryReference",
                table: "Product",
                column: "CategoryReference");

            migrationBuilder.CreateIndex(
                name: "IX_Article_CategoryReference",
                table: "Article",
                column: "CategoryReference");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_NewsCategories_CategoryReference",
                table: "Article",
                column: "CategoryReference",
                principalTable: "NewsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategoryReference",
                table: "Product",
                column: "CategoryReference",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_NewsCategories_CategoryReference",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategoryReference",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CategoryReference",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Article_CategoryReference",
                table: "Article");

            migrationBuilder.AlterColumn<long>(
                name: "TagsReference",
                table: "Article",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryReference",
                table: "Article",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
