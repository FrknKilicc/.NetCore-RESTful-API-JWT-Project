using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestProje.Migrations
{
    public partial class init12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Origins_OriginId",
                table: "Characters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Origins",
                table: "Origins");

            migrationBuilder.RenameTable(
                name: "Origins",
                newName: "Origin");

            migrationBuilder.AlterColumn<string>(
                name: "AirDate",
                table: "Episodess",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Origin",
                table: "Origin",
                column: "OriginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Origin_OriginId",
                table: "Characters",
                column: "OriginId",
                principalTable: "Origin",
                principalColumn: "OriginId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Origin_OriginId",
                table: "Characters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Origin",
                table: "Origin");

            migrationBuilder.RenameTable(
                name: "Origin",
                newName: "Origins");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AirDate",
                table: "Episodess",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Origins",
                table: "Origins",
                column: "OriginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Origins_OriginId",
                table: "Characters",
                column: "OriginId",
                principalTable: "Origins",
                principalColumn: "OriginId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
