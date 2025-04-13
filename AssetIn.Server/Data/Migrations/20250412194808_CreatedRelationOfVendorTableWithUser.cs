using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetIn.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatedRelationOfVendorTableWithUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Vendors",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "25e9d1c9-dc6e-4c55-b544-fd92306b3224");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "1304ee64-3118-4e5c-96e9-a2a8e07f8a40");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "f364fa87-df9a-49dd-a8e0-2562ec35a4d6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "849fba08-c719-4d3e-b4d1-85254a0611e9");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_UserID",
                table: "Vendors",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_AspNetUsers_UserID",
                table: "Vendors",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_AspNetUsers_UserID",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_UserID",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Vendors");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "1f41557f-e62b-4768-a0ff-5423bd105d98");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "a27b62f2-9084-44a5-83a7-7a68c65d9da0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "53fe79c9-fd1b-4462-ba62-469c008695fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "5682b6be-06c9-4410-b87e-31d96c2a15be");
        }
    }
}
