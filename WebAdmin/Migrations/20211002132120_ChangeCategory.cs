using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebAdmin.Migrations
{
    public partial class ChangeCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategoryMain",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK__OrderItem",
                table: "_OrderItem");

            migrationBuilder.AddColumn<string>(
                name: "GroupIdList",
                table: "Service",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CategoryMain",
                table: "Product",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Contact",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContactDate",
                table: "Contact",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CookieID",
                table: "Contact",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAgree",
                table: "Contact",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Contact",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Contact",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Contact",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Contact",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DisplayOnHome",
                table: "Categories",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "_OrderItem",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "_OrderItem",
                type: "float",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                table: "_Order",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "_Order",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CookieID",
                table: "_Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FeeShip",
                table: "_Order",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAgree",
                table: "_Order",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "_Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "_Order",
                type: "float",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__OrderItem",
                table: "_OrderItem",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_StatusId",
                table: "Contact",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX__OrderItem_ProductId",
                table: "_OrderItem",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact__OrderStatus_StatusId",
                table: "Contact",
                column: "StatusId",
                principalTable: "_OrderStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategoryMain",
                table: "Product",
                column: "CategoryMain",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact__OrderStatus_StatusId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategoryMain",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Contact_StatusId",
                table: "Contact");

            migrationBuilder.DropPrimaryKey(
                name: "PK__OrderItem",
                table: "_OrderItem");

            migrationBuilder.DropIndex(
                name: "IX__OrderItem_ProductId",
                table: "_OrderItem");

            migrationBuilder.DropColumn(
                name: "GroupIdList",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ContactDate",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CookieID",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "IsAgree",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "_OrderItem");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "_OrderItem");

            migrationBuilder.DropColumn(
                name: "CookieID",
                table: "_Order");

            migrationBuilder.DropColumn(
                name: "FeeShip",
                table: "_Order");

            migrationBuilder.DropColumn(
                name: "IsAgree",
                table: "_Order");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "_Order");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "_Order");

            migrationBuilder.AlterColumn<long>(
                name: "CategoryMain",
                table: "Product",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Contact",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "DisplayOnHome",
                table: "Categories",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                table: "_Order",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "_Order",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__OrderItem",
                table: "_OrderItem",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategoryMain",
                table: "Product",
                column: "CategoryMain",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
