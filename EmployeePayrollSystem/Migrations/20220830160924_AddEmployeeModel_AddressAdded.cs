using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeePayrollSystem.Migrations
{
    public partial class AddEmployeeModel_AddressAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AddEmployee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AddEmployee");
        }
    }
}
