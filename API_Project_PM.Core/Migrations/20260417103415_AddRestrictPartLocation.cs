using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Project_PM.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddRestrictPartLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Locations_DefaultLocationId",
                table: "Parts");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Locations_DefaultLocationId",
                table: "Parts",
                column: "DefaultLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Locations_DefaultLocationId",
                table: "Parts");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Locations_DefaultLocationId",
                table: "Parts",
                column: "DefaultLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
