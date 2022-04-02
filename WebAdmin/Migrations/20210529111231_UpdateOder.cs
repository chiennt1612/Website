using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAdmin.Migrations
{
    public partial class UpdateOder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Order__Address_AddressId",
                table: "_Order");

            migrationBuilder.DropIndex(
                name: "IX__Order_AddressId",
                table: "_Order");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "_Order");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "_Order",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "_Order",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "_Order",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "_Order",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "_Order",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "_Address",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "_Order");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "_Order");

            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "_Order");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "_Order");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "_Address");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "_Order",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "_Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX__Order_AddressId",
                table: "_Order",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK__Order__Address_AddressId",
                table: "_Order",
                column: "AddressId",
                principalTable: "_Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
