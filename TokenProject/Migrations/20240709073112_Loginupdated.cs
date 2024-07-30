using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TokenProject.Migrations
{
    /// <inheritdoc />
    public partial class Loginupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "LoginUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "LoginUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_LoginUsers_CustomerId",
                table: "LoginUsers",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginUsers_Customers_CustomerId",
                table: "LoginUsers",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginUsers_Customers_CustomerId",
                table: "LoginUsers");

            migrationBuilder.DropIndex(
                name: "IX_LoginUsers_CustomerId",
                table: "LoginUsers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "LoginUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "LoginUsers");
        }
    }
}
