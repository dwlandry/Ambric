using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instrument_Spec_Manager.Module.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneAndAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumber_Employees_EmployeeID",
                table: "PhoneNumber");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneNumber",
                table: "PhoneNumber");

            migrationBuilder.RenameTable(
                name: "PhoneNumber",
                newName: "PhoneNumbers");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumber_EmployeeID",
                table: "PhoneNumbers",
                newName: "IX_PhoneNumbers_EmployeeID");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressID",
                table: "Employees",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneNumbers",
                table: "PhoneNumbers",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    StateProvince = table.Column<string>(type: "text", nullable: true),
                    ZipPostal = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AddressID",
                table: "Employees",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Addresses_AddressID",
                table: "Employees",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Employees_EmployeeID",
                table: "PhoneNumbers",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Addresses_AddressID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Employees_EmployeeID",
                table: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AddressID",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneNumbers",
                table: "PhoneNumbers");

            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "PhoneNumbers",
                newName: "PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumbers_EmployeeID",
                table: "PhoneNumber",
                newName: "IX_PhoneNumber_EmployeeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneNumber",
                table: "PhoneNumber",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumber_Employees_EmployeeID",
                table: "PhoneNumber",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "ID");
        }
    }
}
