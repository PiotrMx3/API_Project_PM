using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Project_PM.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddRestrictPartSupplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartSuppliers_Parts_PartId",
                table: "PartSuppliers");

            migrationBuilder.AddForeignKey(
                name: "FK_PartSuppliers_Parts_PartId",
                table: "PartSuppliers",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartSuppliers_Parts_PartId",
                table: "PartSuppliers");

            migrationBuilder.AddForeignKey(
                name: "FK_PartSuppliers_Parts_PartId",
                table: "PartSuppliers",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
