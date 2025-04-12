using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetIn.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssetStatusTableAndAssetTableAddedAndRemovedRequiredColumnsInRespectedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_OrganizationsAssetStatuses_AssetStatuslD",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Organizations_OrganizationlD",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationsAssetStatuses_Organizations_OrganizationsID",
                table: "OrganizationsAssetStatuses");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationsAssetStatuses_OrganizationsID",
                table: "OrganizationsAssetStatuses");

            migrationBuilder.DropColumn(
                name: "OrganizationsID",
                table: "OrganizationsAssetStatuses");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "OrganizationsAssetAssignReturns",
                newName: "ReturnedAt");

            migrationBuilder.RenameColumn(
                name: "OrganizationlD",
                table: "Assets",
                newName: "OrganizationID");

            migrationBuilder.RenameColumn(
                name: "AssetStatuslD",
                table: "Assets",
                newName: "AssetStatusID");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_OrganizationlD",
                table: "Assets",
                newName: "IX_Assets_OrganizationID");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_AssetStatuslD",
                table: "Assets",
                newName: "IX_Assets_AssetStatusID");

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedAt",
                table: "OrganizationsAssetAssignReturns",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "DeletedByOrganization",
                table: "Assets",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.InsertData(
                table: "OrganizationsAssetStatuses",
                columns: new[] { "OrganizationsAssetStatusID", "OrganizationsAssetStatusName" },
                values: new object[,]
                {
                    { 1, "Assigned" },
                    { 2, "Retired" },
                    { 3, "UnderMaintenance" },
                    { 4, "Available" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_OrganizationsAssetStatuses_AssetStatusID",
                table: "Assets",
                column: "AssetStatusID",
                principalTable: "OrganizationsAssetStatuses",
                principalColumn: "OrganizationsAssetStatusID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Organizations_OrganizationID",
                table: "Assets",
                column: "OrganizationID",
                principalTable: "Organizations",
                principalColumn: "OrganizationID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_OrganizationsAssetStatuses_AssetStatusID",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Organizations_OrganizationID",
                table: "Assets");

            migrationBuilder.DeleteData(
                table: "OrganizationsAssetStatuses",
                keyColumn: "OrganizationsAssetStatusID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrganizationsAssetStatuses",
                keyColumn: "OrganizationsAssetStatusID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrganizationsAssetStatuses",
                keyColumn: "OrganizationsAssetStatusID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrganizationsAssetStatuses",
                keyColumn: "OrganizationsAssetStatusID",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "AssignedAt",
                table: "OrganizationsAssetAssignReturns");

            migrationBuilder.DropColumn(
                name: "DeletedByOrganization",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "ReturnedAt",
                table: "OrganizationsAssetAssignReturns",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "OrganizationID",
                table: "Assets",
                newName: "OrganizationlD");

            migrationBuilder.RenameColumn(
                name: "AssetStatusID",
                table: "Assets",
                newName: "AssetStatuslD");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_OrganizationID",
                table: "Assets",
                newName: "IX_Assets_OrganizationlD");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_AssetStatusID",
                table: "Assets",
                newName: "IX_Assets_AssetStatuslD");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationsID",
                table: "OrganizationsAssetStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_OrganizationsAssetStatuses_OrganizationsID",
                table: "OrganizationsAssetStatuses",
                column: "OrganizationsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_OrganizationsAssetStatuses_AssetStatuslD",
                table: "Assets",
                column: "AssetStatuslD",
                principalTable: "OrganizationsAssetStatuses",
                principalColumn: "OrganizationsAssetStatusID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Organizations_OrganizationlD",
                table: "Assets",
                column: "OrganizationlD",
                principalTable: "Organizations",
                principalColumn: "OrganizationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationsAssetStatuses_Organizations_OrganizationsID",
                table: "OrganizationsAssetStatuses",
                column: "OrganizationsID",
                principalTable: "Organizations",
                principalColumn: "OrganizationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
