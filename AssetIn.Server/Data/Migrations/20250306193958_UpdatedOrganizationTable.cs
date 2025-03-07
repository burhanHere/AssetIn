using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetIn.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOrganizationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndustryType",
                table: "Organizations");

            migrationBuilder.AddColumn<bool>(
                name: "ActiveOrganization",
                table: "Organizations",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Organizations",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "8f9eba3c-5209-4040-a8c4-0d02bf54f28a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "4cbd1456-b822-45a5-8617-983ffda9b910");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "f3f177fa-0d84-41ae-bcf5-e5e36d9f2e27");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "01c9200f-0376-474f-884b-0281ec7b9752");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_UserID",
                table: "Organizations",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_AspNetUsers_UserID",
                table: "Organizations",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_AspNetUsers_UserID",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_UserID",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "ActiveOrganization",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Organizations");

            migrationBuilder.AddColumn<string>(
                name: "IndustryType",
                table: "Organizations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "e40b7007-b10e-420f-b6f2-113111a86be6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "305a186d-d03c-4ba1-b4cc-18bffc031a7d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "80fc609c-b6ac-49ee-973a-beb175216d2f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "9523eb6f-c27e-457c-91fd-66bf09b225c1");
        }
    }
}
