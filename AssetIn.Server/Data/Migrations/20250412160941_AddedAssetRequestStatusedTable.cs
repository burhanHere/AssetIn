using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetIn.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAssetRequestStatusedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_OrganizationsAssetStatuses_AssetStatusID",
                table: "Assets");

            migrationBuilder.CreateTable(
                name: "OrganizationsAssetRequestStatuses",
                columns: table => new
                {
                    OrganizationsAssetRequestStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrganizationsAssetRequestStatusName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationsAssetRequestStatuses", x => x.OrganizationsAssetRequestStatusID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.InsertData(
                table: "OrganizationsAssetRequestStatuses",
                columns: new[] { "OrganizationsAssetRequestStatusID", "OrganizationsAssetRequestStatusName" },
                values: new object[,]
                {
                    { 1, "Accepted" },
                    { 2, "Pending" },
                    { 3, "Declined" },
                    { 4, "Fulfilled" },
                    { 5, "Canceled" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_OrganizationsAssetRequestStatuses_AssetStatusID",
                table: "Assets",
                column: "AssetStatusID",
                principalTable: "OrganizationsAssetRequestStatuses",
                principalColumn: "OrganizationsAssetRequestStatusID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_OrganizationsAssetRequestStatuses_AssetStatusID",
                table: "Assets");

            migrationBuilder.DropTable(
                name: "OrganizationsAssetRequestStatuses");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "4d49d309-9a18-4b5e-93ba-6292297ae743");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "b39c7ac2-04d2-45a8-8906-9b79ece6f48b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "5a677668-afe6-49e9-88f7-22ab7b8d9c5e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "5a9b68a7-a14e-4050-affe-34200663878f");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_OrganizationsAssetStatuses_AssetStatusID",
                table: "Assets",
                column: "AssetStatusID",
                principalTable: "OrganizationsAssetStatuses",
                principalColumn: "OrganizationsAssetStatusID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
