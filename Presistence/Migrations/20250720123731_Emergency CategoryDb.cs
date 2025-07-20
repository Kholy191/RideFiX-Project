using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class EmergencyCategoryDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "categoryId",
                table: "emergencyRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_emergencyRequests_categoryId",
                table: "emergencyRequests",
                column: "categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_emergencyRequests_categories_categoryId",
                table: "emergencyRequests",
                column: "categoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_emergencyRequests_categories_categoryId",
                table: "emergencyRequests");

            migrationBuilder.DropIndex(
                name: "IX_emergencyRequests_categoryId",
                table: "emergencyRequests");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "emergencyRequests");
        }
    }
}
