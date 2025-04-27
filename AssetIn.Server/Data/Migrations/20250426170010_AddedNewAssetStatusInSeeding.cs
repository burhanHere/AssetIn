using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetIn.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewAssetStatusInSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "061a2e04-55c1-4ffc-9847-f6608d374ca2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "8d68cf56-db52-47fe-bcb3-5422fcd5e15c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "cdbaee74-ce44-4775-9db0-2294ef87d03f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "66b60031-9684-430c-b130-1c34768aed65");

            migrationBuilder.UpdateData(
                table: "OrganizationsAssetStatuses",
                keyColumn: "OrganizationsAssetStatusID",
                keyValue: 3,
                column: "OrganizationsAssetStatusName",
                value: "Under Maintenance");

            migrationBuilder.InsertData(
                table: "OrganizationsAssetStatuses",
                columns: new[] { "OrganizationsAssetStatusID", "OrganizationsAssetStatusName" },
                values: new object[,]
                {
                    { 5, "Lost" },
                    { 6, "Out Of Order" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrganizationsAssetStatuses",
                keyColumn: "OrganizationsAssetStatusID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrganizationsAssetStatuses",
                keyColumn: "OrganizationsAssetStatusID",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "6244f5ba-197e-4777-b01c-2a3b49114549");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "b1d505c7-4137-43e8-aac8-0fe395cea464");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "49fd5171-f217-46ba-b834-d1be697ce597");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "665c4599-d7ec-4678-82d8-5d0e0de6a3a5");

            migrationBuilder.UpdateData(
                table: "OrganizationsAssetStatuses",
                keyColumn: "OrganizationsAssetStatusID",
                keyValue: 3,
                column: "OrganizationsAssetStatusName",
                value: "UnderMaintenance");
        }
    }
}
