using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateUserForignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_photoId",
                table: "AspNetUsers",
                column: "photoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FileDetails_photoId",
                table: "AspNetUsers",
                column: "photoId",
                principalTable: "FileDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FileDetails_photoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_photoId",
                table: "AspNetUsers");
        }
    }
}
