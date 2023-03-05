using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wheel_OF_Fate.DA.Migrations
{
    public partial class AddShiftToDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkingHours",
                table: "EmployeeWorkingHours",
                newName: "WorkingShift");

            migrationBuilder.CreateTable(
                name: "shifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shifts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shifts");

            migrationBuilder.RenameColumn(
                name: "WorkingShift",
                table: "EmployeeWorkingHours",
                newName: "WorkingHours");
        }
    }
}
