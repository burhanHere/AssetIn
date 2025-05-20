using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetIn.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemovedOrganizationDomainTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationsDomains");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "12c502cd-72e9-492f-99c3-932929411310");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "e0f6836b-2686-4fb3-82c7-ce1a6cd1ff30");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "3faf3175-389a-4124-9c52-e3492ea5aba1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "46203bc0-b1d8-4bc1-aeba-28ba330a8e83");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationsDomains",
                columns: table => new
                {
                    OrganizationsDomainID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrganizationsID = table.Column<int>(type: "int", nullable: false),
                    Domain = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationsDomains", x => x.OrganizationsDomainID);
                    table.ForeignKey(
                        name: "FK_OrganizationsDomains_Organizations_OrganizationsID",
                        column: x => x.OrganizationsID,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationID",
                        onDelete: ReferentialAction.Cascade);
                })
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

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationsDomains_OrganizationsID",
                table: "OrganizationsDomains",
                column: "OrganizationsID");
        }
    }
}
