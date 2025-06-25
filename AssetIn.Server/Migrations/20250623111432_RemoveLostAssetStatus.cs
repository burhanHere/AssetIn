using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetIn.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLostAssetStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrganizationsAssetStatuses",
                keyColumn: "OrganizationsAssetStatusID",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "a5d98478-2941-4901-b628-e2f22e4db92c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "9a0e34ec-76b7-4688-ae34-11e1f3f1b3ef");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "8fa520e0-5f39-497d-8d49-dfcc00f70243");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "bf9e717b-5ba2-4b14-bd2f-f61ea80d8e2e");

            migrationBuilder.UpdateData(
                table: "OrganizationsAssetStatuses",
                keyColumn: "OrganizationsAssetStatusID",
                keyValue: 5,
                column: "OrganizationsAssetStatusName",
                value: "Out Of Order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "cc3ad06b-7160-44a5-93b5-195321e3d89b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "f5cd44a9-7cb4-4612-988a-4a9bbc667e43");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "22ec94a9-a17f-4526-850f-ba394e75a388");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "1ffda9d9-c29f-4fe2-bd82-555f54c77396");

            migrationBuilder.UpdateData(
                table: "OrganizationsAssetStatuses",
                keyColumn: "OrganizationsAssetStatusID",
                keyValue: 5,
                column: "OrganizationsAssetStatusName",
                value: "Lost");

            migrationBuilder.InsertData(
                table: "OrganizationsAssetStatuses",
                columns: new[] { "OrganizationsAssetStatusID", "OrganizationsAssetStatusName" },
                values: new object[] { 6, "Out Of Order" });
        }
    }
}
