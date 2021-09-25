using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MagdyClinic.Migrations
{
    public partial class AddPsandScheduleandSlot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorScheduleCriteria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorScheduleCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorScheduleCriteria_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PainSeverity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PainScale = table.Column<int>(type: "int", nullable: false),
                    PainLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayOrNight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PainRadiation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PainSeverityTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PainSeverity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PainSeverity_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Slot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsTaken = table.Column<bool>(type: "bit", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorScheduleCriteriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slot_DoctorScheduleCriteria_DoctorScheduleCriteriaId",
                        column: x => x.DoctorScheduleCriteriaId,
                        principalTable: "DoctorScheduleCriteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorScheduleCriteria_DoctorId",
                table: "DoctorScheduleCriteria",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PainSeverity_PatientId",
                table: "PainSeverity",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Slot_DoctorScheduleCriteriaId",
                table: "Slot",
                column: "DoctorScheduleCriteriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PainSeverity");

            migrationBuilder.DropTable(
                name: "Slot");

            migrationBuilder.DropTable(
                name: "DoctorScheduleCriteria");
        }
    }
}
