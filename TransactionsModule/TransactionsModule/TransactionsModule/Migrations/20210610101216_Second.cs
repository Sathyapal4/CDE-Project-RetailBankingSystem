using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionsModule.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Service_ID",
                keyValue: 1,
                column: "Date_Service_Provided",
                value: new DateTime(2021, 6, 10, 15, 42, 16, 103, DateTimeKind.Local).AddTicks(4678));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Service_ID",
                keyValue: 2,
                column: "Date_Service_Provided",
                value: new DateTime(2021, 6, 10, 15, 42, 16, 103, DateTimeKind.Local).AddTicks(5527));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Service_ID",
                keyValue: 3,
                column: "Date_Service_Provided",
                value: new DateTime(2021, 6, 10, 15, 42, 16, 103, DateTimeKind.Local).AddTicks(5555));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Service_ID",
                keyValue: 1,
                column: "Date_Service_Provided",
                value: new DateTime(2021, 6, 8, 20, 5, 33, 485, DateTimeKind.Local).AddTicks(3680));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Service_ID",
                keyValue: 2,
                column: "Date_Service_Provided",
                value: new DateTime(2021, 6, 8, 20, 5, 33, 485, DateTimeKind.Local).AddTicks(4469));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Service_ID",
                keyValue: 3,
                column: "Date_Service_Provided",
                value: new DateTime(2021, 6, 8, 20, 5, 33, 485, DateTimeKind.Local).AddTicks(4483));
        }
    }
}
