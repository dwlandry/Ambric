using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instrument_Spec_Manager.Module.Migrations
{
    /// <inheritdoc />
    public partial class AddResumeAndPortfolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileData",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Resumes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uuid", nullable: true),
                    FileID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Resumes_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Resumes_FileData_FileID",
                        column: x => x.FileID,
                        principalTable: "FileData",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FileAttachments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    FileID = table.Column<Guid>(type: "uuid", nullable: true),
                    ResumeID = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAttachments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FileAttachments_FileData_FileID",
                        column: x => x.FileID,
                        principalTable: "FileData",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FileAttachments_Resumes_ResumeID",
                        column: x => x.ResumeID,
                        principalTable: "Resumes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_FileID",
                table: "FileAttachments",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_ResumeID",
                table: "FileAttachments",
                column: "ResumeID");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_EmployeeID",
                table: "Resumes",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_FileID",
                table: "Resumes",
                column: "FileID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileAttachments");

            migrationBuilder.DropTable(
                name: "Resumes");

            migrationBuilder.DropTable(
                name: "FileData");
        }
    }
}
