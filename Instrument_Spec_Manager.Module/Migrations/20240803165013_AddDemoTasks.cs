﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instrument_Spec_Manager.Module.Migrations
{
    /// <inheritdoc />
    public partial class AddDemoTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DemoTasks",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCompleted = table.Column<DateTime>(type: "date", nullable: true),
                    Subject = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DueDate = table.Column<DateTime>(type: "date", nullable: true),
                    StartDate = table.Column<DateTime>(type: "date", nullable: true),
                    PercentCompleted = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemoTasks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DemoTaskEmployee",
                columns: table => new
                {
                    DemoTasksID = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeesID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemoTaskEmployee", x => new { x.DemoTasksID, x.EmployeesID });
                    table.ForeignKey(
                        name: "FK_DemoTaskEmployee_DemoTasks_DemoTasksID",
                        column: x => x.DemoTasksID,
                        principalTable: "DemoTasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DemoTaskEmployee_Employees_EmployeesID",
                        column: x => x.EmployeesID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DemoTaskEmployee_EmployeesID",
                table: "DemoTaskEmployee",
                column: "EmployeesID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemoTaskEmployee");

            migrationBuilder.DropTable(
                name: "DemoTasks");
        }
    }
}
