using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionsModule.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Counterparties",
                columns: table => new
                {
                    Counterparty_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Other_Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counterparties", x => x.Counterparty_ID);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Payment_Methods",
                columns: table => new
                {
                    Payment_Method_Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Payment_Method_Name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_Payment_Methods", x => x.Payment_Method_Code);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Transaction_Status",
                columns: table => new
                {
                    Trans_Status_Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Trans_Status_Description = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_Transaction_Status", x => x.Trans_Status_Code);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Transaction_Types",
                columns: table => new
                {
                    Trans_Type_Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Trans_Type_Description = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_Transaction_Types", x => x.Trans_Type_Code);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Service_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_Service_Provided = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Other_Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Service_ID);
                });

            migrationBuilder.CreateTable(
                name: "Financial_Transactions",
                columns: table => new
                {
                    Transaction_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account_ID = table.Column<int>(type: "int", nullable: false),
                    Counterparty_ID = table.Column<int>(type: "int", nullable: false),
                    Date_of_Transaction = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount_of_Transaction = table.Column<double>(type: "float", nullable: false),
                    Other_Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Payment_Method_Code = table.Column<int>(type: "int", nullable: false),
                    Trans_Type_Code = table.Column<int>(type: "int", nullable: false),
                    Trans_Status_Code = table.Column<int>(type: "int", nullable: false),
                    Service_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financial_Transactions", x => x.Transaction_ID);
                    table.ForeignKey(
                        name: "FK_Financial_Transactions_Counterparties_Counterparty_ID",
                        column: x => x.Counterparty_ID,
                        principalTable: "Counterparties",
                        principalColumn: "Counterparty_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Financial_Transactions_Ref_Payment_Methods_Payment_Method_Code",
                        column: x => x.Payment_Method_Code,
                        principalTable: "Ref_Payment_Methods",
                        principalColumn: "Payment_Method_Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Financial_Transactions_Ref_Transaction_Status_Trans_Status_Code",
                        column: x => x.Trans_Status_Code,
                        principalTable: "Ref_Transaction_Status",
                        principalColumn: "Trans_Status_Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Financial_Transactions_Ref_Transaction_Types_Trans_Type_Code",
                        column: x => x.Trans_Type_Code,
                        principalTable: "Ref_Transaction_Types",
                        principalColumn: "Trans_Type_Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Financial_Transactions_Services_Service_ID",
                        column: x => x.Service_ID,
                        principalTable: "Services",
                        principalColumn: "Service_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Counterparties",
                columns: new[] { "Counterparty_ID", "Other_Details" },
                values: new object[,]
                {
                    { 1, "Other Details 1" },
                    { 2, "Other Details 2" },
                    { 3, "Other Details 3" }
                });

            migrationBuilder.InsertData(
                table: "Ref_Payment_Methods",
                columns: new[] { "Payment_Method_Code", "Payment_Method_Name" },
                values: new object[,]
                {
                    { 5, 4 },
                    { 4, 3 },
                    { 6, 5 },
                    { 2, 1 },
                    { 1, 0 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Ref_Transaction_Status",
                columns: new[] { "Trans_Status_Code", "Trans_Status_Description" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 0 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Ref_Transaction_Types",
                columns: new[] { "Trans_Type_Code", "Trans_Type_Description" },
                values: new object[,]
                {
                    { 1, 0 },
                    { 2, 1 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Service_ID", "Date_Service_Provided", "Other_Details" },
                values: new object[,]
                {
                    { 2, new DateTime(2021, 6, 8, 20, 5, 33, 485, DateTimeKind.Local).AddTicks(4469), "Other Details 1" },
                    { 1, new DateTime(2021, 6, 8, 20, 5, 33, 485, DateTimeKind.Local).AddTicks(3680), "Other Details 1" },
                    { 3, new DateTime(2021, 6, 8, 20, 5, 33, 485, DateTimeKind.Local).AddTicks(4483), "Other Details 1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Financial_Transactions_Counterparty_ID",
                table: "Financial_Transactions",
                column: "Counterparty_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Financial_Transactions_Payment_Method_Code",
                table: "Financial_Transactions",
                column: "Payment_Method_Code");

            migrationBuilder.CreateIndex(
                name: "IX_Financial_Transactions_Service_ID",
                table: "Financial_Transactions",
                column: "Service_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Financial_Transactions_Trans_Status_Code",
                table: "Financial_Transactions",
                column: "Trans_Status_Code");

            migrationBuilder.CreateIndex(
                name: "IX_Financial_Transactions_Trans_Type_Code",
                table: "Financial_Transactions",
                column: "Trans_Type_Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Financial_Transactions");

            migrationBuilder.DropTable(
                name: "Counterparties");

            migrationBuilder.DropTable(
                name: "Ref_Payment_Methods");

            migrationBuilder.DropTable(
                name: "Ref_Transaction_Status");

            migrationBuilder.DropTable(
                name: "Ref_Transaction_Types");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
