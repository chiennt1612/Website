using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAdmin.Migrations
{
    public partial class UpdateParamSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "ParamSetting");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ParamSetting");

            migrationBuilder.DropColumn(
                name: "Hotline",
                table: "ParamSetting");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "ParamSetting");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ParamSetting");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "ParamSetting");

            migrationBuilder.DropColumn(
                name: "Since",
                table: "ParamSetting");

            migrationBuilder.AddColumn<string>(
                name: "ParamKey",
                table: "ParamSetting",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParamValue",
                table: "ParamSetting",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ParamSetting_ParamKey",
                table: "ParamSetting",
                column: "ParamKey",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParamSetting_ParamKey",
                table: "ParamSetting");

            migrationBuilder.DropColumn(
                name: "ParamKey",
                table: "ParamSetting");

            migrationBuilder.DropColumn(
                name: "ParamValue",
                table: "ParamSetting");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ParamSetting",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ParamSetting",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Hotline",
                table: "ParamSetting",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "ParamSetting",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ParamSetting",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "ParamSetting",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Since",
                table: "ParamSetting",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
