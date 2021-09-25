using Microsoft.EntityFrameworkCore.Migrations;

namespace MagdyClinic.Migrations
{
    public partial class DiagnosePatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnose_Doctor_DoctorId",
                table: "Diagnose");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Diagnose_DiagnoseId",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Patient_DiagnoseId",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Diagnose_DoctorId",
                table: "Diagnose");

            migrationBuilder.DropColumn(
                name: "DiagnoseId",
                table: "Patient");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Diagnose",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Diagnose_PatientId",
                table: "Diagnose",
                column: "PatientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnose_Patient_PatientId",
                table: "Diagnose",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnose_Patient_PatientId",
                table: "Diagnose");

            migrationBuilder.DropIndex(
                name: "IX_Diagnose_PatientId",
                table: "Diagnose");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Diagnose");

            migrationBuilder.AddColumn<int>(
                name: "DiagnoseId",
                table: "Patient",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_DiagnoseId",
                table: "Patient",
                column: "DiagnoseId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnose_DoctorId",
                table: "Diagnose",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnose_Doctor_DoctorId",
                table: "Diagnose",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Diagnose_DiagnoseId",
                table: "Patient",
                column: "DiagnoseId",
                principalTable: "Diagnose",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
