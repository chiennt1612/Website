using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebAdmin.Migrations
{
    public partial class UpdateContactAddService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_NewsCategories_CategoryReference",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategoryReference",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Product",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Contact",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ServiceId",
                table: "Contact",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Img = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreator = table.Column<long>(type: "bigint", nullable: false),
                    DateCreator = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModify = table.Column<long>(type: "bigint", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserDeleted = table.Column<long>(type: "bigint", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaKeyword = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaBox = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaRobotTag = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ServiceId",
                table: "Contact",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_NewsCategories_CategoryReference",
                table: "Article",
                column: "CategoryReference",
                principalTable: "NewsCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Service_ServiceId",
                table: "Contact",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategoryReference",
                table: "Product",
                column: "CategoryReference",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_NewsCategories_CategoryReference",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Service_ServiceId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategoryReference",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Contact_ServiceId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Contact");

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Product",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_NewsCategories_CategoryReference",
                table: "Article",
                column: "CategoryReference",
                principalTable: "NewsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategoryReference",
                table: "Product",
                column: "CategoryReference",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
