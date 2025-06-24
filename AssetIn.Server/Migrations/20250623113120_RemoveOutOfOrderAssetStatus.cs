using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetIn.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOutOfOrderAssetStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrganizationsAssetStatuses",
                keyColumn: "OrganizationsAssetStatusID",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "25855d14-72bc-4862-8b00-6f62ebccf051");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "8dfb6fd2-01e1-46ff-96e9-8435f3eab95a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "d519f8a6-f957-4156-ba8b-2dd2a2e1d768");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "b15c3daa-ffe1-411b-8913-72a954853433");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "OrganizationsAssetStatuses",
                columns: new[] { "OrganizationsAssetStatusID", "OrganizationsAssetStatusName" },
                values: new object[] { 5, "Out Of Order" });
        }
    }
}
