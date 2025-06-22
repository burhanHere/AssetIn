using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetIn.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedAssignmentIdForigenKeyInAssetRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssetAssignmentId",
                table: "OrganizationsAssetRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CheckInByUserID",
                table: "OrganizationsAssetAssignReturns",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CheckInNotes",
                table: "OrganizationsAssetAssignReturns",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "9e1deadb-2d9d-440d-8a61-efc2f6382c09");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "03558503-7746-4df5-952f-0f1b5f9b922e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "5d9d7755-db3a-4405-b58b-2fc35eec1857");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "59fd15bd-1d5b-41c4-8fea-72380a6b95cd");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationsAssetRequests_AssetAssignmentId",
                table: "OrganizationsAssetRequests",
                column: "AssetAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationsAssetAssignReturns_CheckInByUserID",
                table: "OrganizationsAssetAssignReturns",
                column: "CheckInByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationsAssetAssignReturns_AspNetUsers_CheckInByUserID",
                table: "OrganizationsAssetAssignReturns",
                column: "CheckInByUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationsAssetRequests_OrganizationsAssetAssignReturns_A~",
                table: "OrganizationsAssetRequests",
                column: "AssetAssignmentId",
                principalTable: "OrganizationsAssetAssignReturns",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationsAssetAssignReturns_AspNetUsers_CheckInByUserID",
                table: "OrganizationsAssetAssignReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationsAssetRequests_OrganizationsAssetAssignReturns_A~",
                table: "OrganizationsAssetRequests");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationsAssetRequests_AssetAssignmentId",
                table: "OrganizationsAssetRequests");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationsAssetAssignReturns_CheckInByUserID",
                table: "OrganizationsAssetAssignReturns");

            migrationBuilder.DropColumn(
                name: "AssetAssignmentId",
                table: "OrganizationsAssetRequests");

            migrationBuilder.DropColumn(
                name: "CheckInByUserID",
                table: "OrganizationsAssetAssignReturns");

            migrationBuilder.DropColumn(
                name: "CheckInNotes",
                table: "OrganizationsAssetAssignReturns");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "4483db94-da99-4984-a385-c0ef474859f9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "e476bbe6-51df-44cb-93b5-8afffd5badc6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "31564a02-ea5a-4105-a1ea-b01256a6f8df");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "42530e79-cdd9-4c1c-95a0-85306a391fdd");
        }
    }
}
