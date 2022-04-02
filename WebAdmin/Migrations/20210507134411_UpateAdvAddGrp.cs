using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAdmin.Migrations
{
    public partial class UpateAdvAddGrp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvPosition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvPosition", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adv_Pos",
                table: "Adv",
                column: "Pos");

            migrationBuilder.AddForeignKey(
                name: "FK_Adv_AdvPosition_Pos",
                table: "Adv",
                column: "Pos",
                principalTable: "AdvPosition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adv_AdvPosition_Pos",
                table: "Adv");

            migrationBuilder.DropTable(
                name: "AdvPosition");

            migrationBuilder.DropIndex(
                name: "IX_Adv_Pos",
                table: "Adv");
        }
    }
}
