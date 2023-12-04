using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestProje.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Characters_LocationId",
                table: "Characters",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Locations_LocationId",
                table: "Characters",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Locations_LocationId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_LocationId",
                table: "Characters");
        }
    }
}
