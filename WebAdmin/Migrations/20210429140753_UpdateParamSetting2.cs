using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAdmin.Migrations
{
    public partial class UpdateParamSetting2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParamSetting_ParamKey",
                table: "ParamSetting");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "ParamSetting",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ParamSetting_ParamKey_Language",
                table: "ParamSetting",
                columns: new[] { "ParamKey", "Language" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParamSetting_ParamKey_Language",
                table: "ParamSetting");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "ParamSetting");

            migrationBuilder.CreateIndex(
                name: "IX_ParamSetting_ParamKey",
                table: "ParamSetting",
                column: "ParamKey",
                unique: true);
        }
    }
}
