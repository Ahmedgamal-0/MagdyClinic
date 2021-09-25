using Microsoft.EntityFrameworkCore.Migrations;

namespace MagdyClinic.Migrations
{
    public partial class AddDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SlotDuration",
                table: "DoctorScheduleCriteria",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlotDuration",
                table: "DoctorScheduleCriteria");
        }
    }
}
