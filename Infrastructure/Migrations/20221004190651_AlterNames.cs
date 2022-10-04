using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AlterNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CanteenEmployees",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "CanteenEmployees",
                newName: "CanteenEmployeeId");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "CanteenEmployees",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "CanteenEmployees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "DateOfBirth", "EmailAddress", "FirstName", "LastName", "PhoneNumber", "StudentNumber", "StudyCity" },
                values: new object[] { 1, new DateTime(2000, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "m.vandercaaij@student.avans.nl", "Mike", "van der Caaij", "0612345678", 2184147, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "CanteenEmployees");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "CanteenEmployees",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CanteenEmployeeId",
                table: "CanteenEmployees",
                newName: "ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "CanteenEmployees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
