using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartEvent.Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class edit_qr_details_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsQrVerificationActive",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "QrCodeId",
                table: "Attendances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_QrCodeId",
                table: "Attendances",
                column: "QrCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_QrCodes_QrCodeId",
                table: "Attendances",
                column: "QrCodeId",
                principalTable: "QrCodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_QrCodes_QrCodeId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_QrCodeId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsQrVerificationActive",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "QrCodeId",
                table: "Attendances");
        }
    }
}
