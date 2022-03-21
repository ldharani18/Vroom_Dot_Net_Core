using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vroom.Migrations
{
    public partial class UpdatingMakeIDtoMakeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Makes_MakeID",
                table: "Models");

            migrationBuilder.RenameColumn(
                name: "MakeID",
                table: "Models",
                newName: "MakeId");

            migrationBuilder.RenameIndex(
                name: "IX_Models_MakeID",
                table: "Models",
                newName: "IX_Models_MakeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Makes_MakeId",
                table: "Models",
                column: "MakeId",
                principalTable: "Makes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Makes_MakeId",
                table: "Models");

            migrationBuilder.RenameColumn(
                name: "MakeId",
                table: "Models",
                newName: "MakeID");

            migrationBuilder.RenameIndex(
                name: "IX_Models_MakeId",
                table: "Models",
                newName: "IX_Models_MakeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Makes_MakeID",
                table: "Models",
                column: "MakeID",
                principalTable: "Makes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
