using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gryffindor.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Customer_ID = table.Column<int>(nullable: false),
                    Customer_Company = table.Column<string>(unicode: false, nullable: true),
                    Customer_Address = table.Column<string>(unicode: false, nullable: true),
                    Customer_Tel = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Customer_ID);
                });

            migrationBuilder.CreateTable(
                name: "Date_Mount",
                columns: table => new
                {
                    Month_ID = table.Column<int>(nullable: false),
                    Month_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Date_Mount", x => x.Month_ID);
                });

            migrationBuilder.CreateTable(
                name: "Date_yearoff",
                columns: table => new
                {
                    Date_ID = table.Column<int>(nullable: false),
                    Date_dateoff = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Date_yearoff", x => x.Date_ID);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Employee_ID = table.Column<int>(nullable: false),
                    Employee_Name = table.Column<string>(unicode: false, nullable: true),
                    Employee_Position = table.Column<string>(unicode: false, nullable: true),
                    Employee_Saraly = table.Column<double>(nullable: true),
                    Employee_Sex = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Employee_Department = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Employee_ID);
                });

            migrationBuilder.CreateTable(
                name: "Issue",
                columns: table => new
                {
                    issue_id = table.Column<int>(nullable: false),
                    issue_name = table.Column<string>(unicode: false, nullable: true),
                    issue_status = table.Column<string>(unicode: false, nullable: true),
                    Employee_name = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issue", x => x.issue_id);
                });

            migrationBuilder.CreateTable(
                name: "Machine",
                columns: table => new
                {
                    Machine_ID = table.Column<int>(nullable: false),
                    Machine_Name = table.Column<string>(unicode: false, nullable: true),
                    Machine_QTY = table.Column<int>(nullable: true),
                    Machine_Time = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machine", x => x.Machine_ID);
                });

            migrationBuilder.CreateTable(
                name: "Materail",
                columns: table => new
                {
                    Materail_ID = table.Column<int>(nullable: false),
                    Materail_Name = table.Column<string>(unicode: false, nullable: true),
                    Materail_Color = table.Column<string>(unicode: false, nullable: true),
                    Materail_QTY = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materail", x => x.Materail_ID);
                });

            migrationBuilder.CreateTable(
                name: "Product_status",
                columns: table => new
                {
                    Status_ID = table.Column<int>(nullable: false),
                    Status_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_status", x => x.Status_ID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Product_ID = table.Column<int>(nullable: false),
                    Product_Name = table.Column<string>(unicode: false, nullable: true),
                    Product_Size = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Product_Color = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Materail_ID = table.Column<int>(nullable: true),
                    Marerail_Name = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Product_ID);
                    table.ForeignKey(
                        name: "FK_Product_Materail1",
                        column: x => x.Materail_ID,
                        principalTable: "Materail",
                        principalColumn: "Materail_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Order_ID = table.Column<int>(nullable: false),
                    Order_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Order_QTY = table.Column<int>(nullable: true),
                    Customer_ID = table.Column<int>(nullable: true),
                    Customer_Company = table.Column<string>(unicode: false, nullable: true),
                    Materail_ID = table.Column<int>(nullable: true),
                    Materail_Name = table.Column<string>(unicode: false, nullable: true),
                    Materail_Color = table.Column<string>(unicode: false, nullable: true),
                    Product_ID = table.Column<int>(nullable: true),
                    Product_Name = table.Column<string>(unicode: false, nullable: true),
                    Product_Size = table.Column<int>(nullable: true),
                    Product_Color = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Employee_ID = table.Column<int>(nullable: true),
                    Employee_Name = table.Column<string>(unicode: false, nullable: true),
                    Employee_Department = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Order_ID);
                    table.ForeignKey(
                        name: "FK_Order_Customer",
                        column: x => x.Customer_ID,
                        principalTable: "Customer",
                        principalColumn: "Customer_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Employee",
                        column: x => x.Employee_ID,
                        principalTable: "Employee",
                        principalColumn: "Employee_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Product",
                        column: x => x.Product_ID,
                        principalTable: "Product",
                        principalColumn: "Product_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Purchase_Request",
                columns: table => new
                {
                    Purchase_ID = table.Column<int>(nullable: false),
                    Phurchse_Doc = table.Column<DateTime>(type: "datetime", nullable: true),
                    Phurchase_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Customer_ID = table.Column<int>(nullable: true),
                    Customer_Company = table.Column<string>(unicode: false, nullable: true),
                    Product_ID = table.Column<int>(nullable: true),
                    Product_Name = table.Column<string>(unicode: false, nullable: true),
                    Materail_ID = table.Column<int>(nullable: true),
                    Materail_name = table.Column<string>(unicode: false, nullable: true),
                    Order_ID = table.Column<int>(nullable: true),
                    Order_QTY = table.Column<int>(nullable: true),
                    Date_ID = table.Column<int>(nullable: true),
                    Date_dateoff = table.Column<DateTime>(type: "date", nullable: true),
                    Month_ID = table.Column<int>(nullable: true),
                    Month_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Status_ID = table.Column<int>(nullable: true),
                    Status_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Delivery = table.Column<int>(nullable: true),
                    Purchase_Total = table.Column<int>(nullable: true),
                    Machine_ID = table.Column<int>(nullable: true),
                    Machine_Name = table.Column<string>(unicode: false, nullable: true),
                    Machine_Time = table.Column<DateTime>(type: "datetime", nullable: true),
                    issue_id = table.Column<int>(nullable: true),
                    issue_name = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase_Request", x => x.Purchase_ID);
                    table.ForeignKey(
                        name: "FK_Purchase_Request_Customer",
                        column: x => x.Customer_ID,
                        principalTable: "Customer",
                        principalColumn: "Customer_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchase_Request_Date_yearoff",
                        column: x => x.Date_ID,
                        principalTable: "Date_yearoff",
                        principalColumn: "Date_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchase_Request_Issue",
                        column: x => x.issue_id,
                        principalTable: "Issue",
                        principalColumn: "issue_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchase_Request_Machine",
                        column: x => x.Machine_ID,
                        principalTable: "Machine",
                        principalColumn: "Machine_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchase_Request_Order",
                        column: x => x.Order_ID,
                        principalTable: "Order",
                        principalColumn: "Order_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchase_Request_Product_status",
                        column: x => x.Status_ID,
                        principalTable: "Product_status",
                        principalColumn: "Status_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule_plan",
                columns: table => new
                {
                    Schedule_ID = table.Column<int>(nullable: false),
                    Purchase_ID = table.Column<int>(nullable: true),
                    Month_ID = table.Column<int>(nullable: true),
                    Month_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Shedule_Total = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule_plan", x => x.Schedule_ID);
                    table.ForeignKey(
                        name: "FK_Schedule_plan_Date_Mount",
                        column: x => x.Month_ID,
                        principalTable: "Date_Mount",
                        principalColumn: "Month_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedule_plan_Purchase_Request",
                        column: x => x.Purchase_ID,
                        principalTable: "Purchase_Request",
                        principalColumn: "Purchase_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_Customer_ID",
                table: "Order",
                column: "Customer_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Employee_ID",
                table: "Order",
                column: "Employee_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Product_ID",
                table: "Order",
                column: "Product_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Materail_ID",
                table: "Product",
                column: "Materail_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Request_Customer_ID",
                table: "Purchase_Request",
                column: "Customer_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Request_Date_ID",
                table: "Purchase_Request",
                column: "Date_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Request_issue_id",
                table: "Purchase_Request",
                column: "issue_id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Request_Machine_ID",
                table: "Purchase_Request",
                column: "Machine_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Request_Order_ID",
                table: "Purchase_Request",
                column: "Order_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Request_Status_ID",
                table: "Purchase_Request",
                column: "Status_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_plan_Month_ID",
                table: "Schedule_plan",
                column: "Month_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_plan_Purchase_ID",
                table: "Schedule_plan",
                column: "Purchase_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedule_plan");

            migrationBuilder.DropTable(
                name: "Date_Mount");

            migrationBuilder.DropTable(
                name: "Purchase_Request");

            migrationBuilder.DropTable(
                name: "Date_yearoff");

            migrationBuilder.DropTable(
                name: "Issue");

            migrationBuilder.DropTable(
                name: "Machine");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product_status");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Materail");
        }
    }
}
