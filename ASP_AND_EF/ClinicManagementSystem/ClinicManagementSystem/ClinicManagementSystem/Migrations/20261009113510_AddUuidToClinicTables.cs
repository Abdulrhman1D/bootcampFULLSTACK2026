using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddUuidToClinicTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Jobs_JobId",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Specialties",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Jobs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
            UPDATE [Users]
            SET [Uuid] = CONVERT(nvarchar(36), NEWID());

            UPDATE [Specialties]
            SET [Uuid] = CONVERT(nvarchar(36), NEWID());

            UPDATE [Patients]
            SET [Uuid] = CONVERT(nvarchar(36), NEWID());

            UPDATE [Jobs]
            SET [Uuid] = CONVERT(nvarchar(36), NEWID());

            UPDATE [Doctors]
            SET [Uuid] = CONVERT(nvarchar(36), NEWID());

            UPDATE [Appointments]
            SET [Uuid] = CONVERT(nvarchar(36), NEWID());
            ");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Uuid",
                table: "Users",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_Uuid",
                table: "Specialties",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Uuid",
                table: "Patients",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_Uuid",
                table: "Jobs",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Uuid",
                table: "Doctors",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Uuid",
                table: "Appointments",
                column: "Uuid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Jobs_JobId",
                table: "Doctors",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Jobs_JobId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Users_Uuid",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Specialties_Uuid",
                table: "Specialties");

            migrationBuilder.DropIndex(
                name: "IX_Patients_Uuid",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_Uuid",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_Uuid",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_Uuid",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Jobs_JobId",
                table: "Doctors",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
