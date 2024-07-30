using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TokenProject.Migrations
{
    /// <inheritdoc />
    public partial class Seeee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "LoginUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LoginUsers_OrderId",
                table: "LoginUsers",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LoginUsers_Orders_OrderId",
                table: "LoginUsers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginUsers_Orders_OrderId",
                table: "LoginUsers");

            migrationBuilder.DropIndex(
                name: "IX_LoginUsers_OrderId",
                table: "LoginUsers");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "LoginUsers");
        }
    }
}
