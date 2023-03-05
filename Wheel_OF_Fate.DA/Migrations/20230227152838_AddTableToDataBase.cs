using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wheel_OF_Fate.DA.Migrations
{
    public partial class AddTableToDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmpId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeWorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkingHours = table.Column<int>(type: "int", nullable: false),
                    WorkedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmpsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeWorkingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeWorkingHours_Employees_EmpsId",
                        column: x => x.EmpsId,
                        principalTable: "Employees",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeWorkingHours_EmpsId",
                table: "EmployeeWorkingHours",
                column: "EmpsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeWorkingHours");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
