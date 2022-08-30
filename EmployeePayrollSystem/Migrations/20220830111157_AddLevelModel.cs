using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeePayrollSystem.Migrations
{
    public partial class AddLevelModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddLevel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    YearlySalaryIncreasePercentage = table.Column<double>(type: "float", nullable: false),
                    TravelAllowance = table.Column<double>(type: "float", nullable: false),
                    MedicalAllowance = table.Column<double>(type: "float", nullable: false),
                    InternetAllowance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddLevel", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddLevel");
        }
    }
}
