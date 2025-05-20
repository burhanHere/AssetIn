using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetIn.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedGenderDateOfBorthCOlumnUnUserTableAndOrganizationDomainColumnInOrganizationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrganizationDomain",
                table: "Organizations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "e06d90c7-b81c-48ab-b876-f4b38d939545");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "12920abb-7cd9-4796-a87e-f4f63e28440b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "c5a722b6-4621-4ac7-9561-8be8b5fc59d3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "cbd66708-7d30-4576-ba02-e1ad4b714efa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationDomain",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3a7259f3-ab3e-4d94-851d-abf4484a7961");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "c4e3b3d2-b071-417f-b3e4-3705acc3cc1b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "daa414fb-4d86-4f02-808f-2d0283e53ac3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "f57ee743-0db2-488e-85d2-1b81d370fefd");
        }
    }
}
