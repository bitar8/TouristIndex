using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedUserToActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TourismActivities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TourismActivities_UserId",
                table: "TourismActivities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourismActivities_AspNetUsers_UserId",
                table: "TourismActivities",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourismActivities_AspNetUsers_UserId",
                table: "TourismActivities");

            migrationBuilder.DropIndex(
                name: "IX_TourismActivities_UserId",
                table: "TourismActivities");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TourismActivities");
        }
    }
}
