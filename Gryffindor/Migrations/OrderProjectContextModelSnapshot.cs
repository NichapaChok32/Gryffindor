﻿// <auto-generated />
using System;
using Gryffindor.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gryffindor.Migrations
{
    [DbContext(typeof(OrderProjectContext))]
    partial class OrderProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Gryffindor.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnName("Customer_ID");

                    b.Property<string>("CustomerAddress")
                        .HasColumnName("Customer_Address")
                        .IsUnicode(false);

                    b.Property<string>("CustomerCompany")
                        .HasColumnName("Customer_Company")
                        .IsUnicode(false);

                    b.Property<string>("CustomerTel")
                        .HasColumnName("Customer_Tel")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Gryffindor.Models.DateMount", b =>
                {
                    b.Property<int>("MonthId")
                        .HasColumnName("Month_ID");

                    b.Property<string>("MonthName")
                        .HasColumnName("Month_Name")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("MonthId");

                    b.ToTable("Date_Mount");
                });

            modelBuilder.Entity("Gryffindor.Models.DateYearoff", b =>
                {
                    b.Property<int>("DateId")
                        .HasColumnName("Date_ID");

                    b.Property<DateTime?>("DateDateoff")
                        .HasColumnName("Date_dateoff")
                        .HasColumnType("date");

                    b.HasKey("DateId");

                    b.ToTable("Date_yearoff");
                });

            modelBuilder.Entity("Gryffindor.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnName("Employee_ID");

                    b.Property<string>("EmployeeDepartment")
                        .HasColumnName("Employee_Department")
                        .IsUnicode(false);

                    b.Property<string>("EmployeeName")
                        .HasColumnName("Employee_Name")
                        .IsUnicode(false);

                    b.Property<string>("EmployeePosition")
                        .HasColumnName("Employee_Position")
                        .IsUnicode(false);

                    b.Property<double?>("EmployeeSaraly")
                        .HasColumnName("Employee_Saraly");

                    b.Property<string>("EmployeeSex")
                        .HasColumnName("Employee_Sex")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("EmployeeId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Gryffindor.Models.Issue", b =>
                {
                    b.Property<int>("IssueId")
                        .HasColumnName("issue_id");

                    b.Property<string>("EmployeeName")
                        .HasColumnName("Employee_name")
                        .IsUnicode(false);

                    b.Property<string>("IssueName")
                        .HasColumnName("issue_name")
                        .IsUnicode(false);

                    b.Property<string>("IssueStatus")
                        .HasColumnName("issue_status")
                        .IsUnicode(false);

                    b.HasKey("IssueId");

                    b.ToTable("Issue");
                });

            modelBuilder.Entity("Gryffindor.Models.Machine", b =>
                {
                    b.Property<int>("MachineId")
                        .HasColumnName("Machine_ID");

                    b.Property<string>("MachineName")
                        .HasColumnName("Machine_Name")
                        .IsUnicode(false);

                    b.Property<int?>("MachineQty")
                        .HasColumnName("Machine_QTY");

                    b.Property<DateTime?>("MachineTime")
                        .HasColumnName("Machine_Time");

                    b.HasKey("MachineId");

                    b.ToTable("Machine");
                });

            modelBuilder.Entity("Gryffindor.Models.Materail", b =>
                {
                    b.Property<int>("MaterailId")
                        .HasColumnName("Materail_ID");

                    b.Property<string>("MaterailColor")
                        .HasColumnName("Materail_Color")
                        .IsUnicode(false);

                    b.Property<string>("MaterailName")
                        .HasColumnName("Materail_Name")
                        .IsUnicode(false);

                    b.Property<int?>("MaterailQty")
                        .HasColumnName("Materail_QTY");

                    b.HasKey("MaterailId");

                    b.ToTable("Materail");
                });

            modelBuilder.Entity("Gryffindor.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnName("Order_ID");

                    b.Property<string>("CustomerCompany")
                        .HasColumnName("Customer_Company")
                        .IsUnicode(false);

                    b.Property<int?>("CustomerId")
                        .HasColumnName("Customer_ID");

                    b.Property<string>("EmployeeDepartment")
                        .HasColumnName("Employee_Department")
                        .IsUnicode(false);

                    b.Property<int?>("EmployeeId")
                        .HasColumnName("Employee_ID");

                    b.Property<string>("EmployeeName")
                        .HasColumnName("Employee_Name")
                        .IsUnicode(false);

                    b.Property<string>("MaterailColor")
                        .HasColumnName("Materail_Color")
                        .IsUnicode(false);

                    b.Property<int?>("MaterailId")
                        .HasColumnName("Materail_ID");

                    b.Property<string>("MaterailName")
                        .HasColumnName("Materail_Name")
                        .IsUnicode(false);

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnName("Order_Date")
                        .HasColumnType("date");

                    b.Property<int?>("OrderQty")
                        .HasColumnName("Order_QTY");

                    b.Property<string>("ProductColor")
                        .HasColumnName("Product_Color")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int?>("ProductId")
                        .HasColumnName("Product_ID");

                    b.Property<string>("ProductName")
                        .HasColumnName("Product_Name")
                        .IsUnicode(false);

                    b.Property<int?>("ProductSize")
                        .HasColumnName("Product_Size");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProductId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Gryffindor.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnName("Product_ID");

                    b.Property<string>("MarerailName")
                        .HasColumnName("Marerail_Name")
                        .IsUnicode(false);

                    b.Property<int?>("MaterailId")
                        .HasColumnName("Materail_ID");

                    b.Property<string>("ProductColor")
                        .HasColumnName("Product_Color")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("ProductName")
                        .HasColumnName("Product_Name")
                        .IsUnicode(false);

                    b.Property<string>("ProductSize")
                        .HasColumnName("Product_Size")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("ProductId");

                    b.HasIndex("MaterailId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Gryffindor.Models.ProductStatus", b =>
                {
                    b.Property<int>("StatusId")
                        .HasColumnName("Status_ID");

                    b.Property<string>("StatusName")
                        .HasColumnName("Status_Name")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("StatusId");

                    b.ToTable("Product_status");
                });

            modelBuilder.Entity("Gryffindor.Models.PurchaseRequest", b =>
                {
                    b.Property<int>("PurchaseId")
                        .HasColumnName("Purchase_ID");

                    b.Property<string>("CustomerCompany")
                        .HasColumnName("Customer_Company")
                        .IsUnicode(false);

                    b.Property<int?>("CustomerId")
                        .HasColumnName("Customer_ID");

                    b.Property<DateTime?>("DateDateoff")
                        .HasColumnName("Date_dateoff")
                        .HasColumnType("date");

                    b.Property<int?>("DateId")
                        .HasColumnName("Date_ID");

                    b.Property<int?>("Delivery");

                    b.Property<int?>("IssueId")
                        .HasColumnName("issue_id");

                    b.Property<string>("IssueName")
                        .HasColumnName("issue_name")
                        .IsUnicode(false);

                    b.Property<int?>("MachineId")
                        .HasColumnName("Machine_ID");

                    b.Property<string>("MachineName")
                        .HasColumnName("Machine_Name")
                        .IsUnicode(false);

                    b.Property<DateTime?>("MachineTime")
                        .HasColumnName("Machine_Time")
                        .HasColumnType("datetime");

                    b.Property<int?>("MaterailId")
                        .HasColumnName("Materail_ID");

                    b.Property<string>("MaterailName")
                        .HasColumnName("Materail_name")
                        .IsUnicode(false);

                    b.Property<int?>("MonthId")
                        .HasColumnName("Month_ID");

                    b.Property<string>("MonthName")
                        .HasColumnName("Month_Name")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int?>("OrderId")
                        .HasColumnName("Order_ID");

                    b.Property<int?>("OrderQty")
                        .HasColumnName("Order_QTY");

                    b.Property<DateTime?>("PhurchaseDate")
                        .HasColumnName("Phurchase_Date")
                        .HasColumnType("date");

                    b.Property<DateTime?>("PhurchseDoc")
                        .HasColumnName("Phurchse_Doc")
                        .HasColumnType("datetime");

                    b.Property<int?>("ProductId")
                        .HasColumnName("Product_ID");

                    b.Property<string>("ProductName")
                        .HasColumnName("Product_Name")
                        .IsUnicode(false);

                    b.Property<int?>("PurchaseTotal")
                        .HasColumnName("Purchase_Total");

                    b.Property<int?>("StatusId")
                        .HasColumnName("Status_ID");

                    b.Property<string>("StatusName")
                        .HasColumnName("Status_Name")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("PurchaseId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DateId");

                    b.HasIndex("IssueId");

                    b.HasIndex("MachineId");

                    b.HasIndex("OrderId");

                    b.HasIndex("StatusId");

                    b.ToTable("Purchase_Request");
                });

            modelBuilder.Entity("Gryffindor.Models.SchedulePlan", b =>
                {
                    b.Property<int>("ScheduleId")
                        .HasColumnName("Schedule_ID");

                    b.Property<int?>("MonthId")
                        .HasColumnName("Month_ID");

                    b.Property<string>("MonthName")
                        .HasColumnName("Month_Name")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int?>("PurchaseId")
                        .HasColumnName("Purchase_ID");

                    b.Property<int>("SheduleTotal")
                        .HasColumnName("Shedule_Total");

                    b.HasKey("ScheduleId");

                    b.HasIndex("MonthId");

                    b.HasIndex("PurchaseId");

                    b.ToTable("Schedule_plan");
                });

            modelBuilder.Entity("Gryffindor.Models.Order", b =>
                {
                    b.HasOne("Gryffindor.Models.Customer", "Customer")
                        .WithMany("Order")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Order_Customer");

                    b.HasOne("Gryffindor.Models.Employee", "Employee")
                        .WithMany("Order")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK_Order_Employee");

                    b.HasOne("Gryffindor.Models.Product", "Product")
                        .WithMany("Order")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_Order_Product");
                });

            modelBuilder.Entity("Gryffindor.Models.Product", b =>
                {
                    b.HasOne("Gryffindor.Models.Materail", "Materail")
                        .WithMany("Product")
                        .HasForeignKey("MaterailId")
                        .HasConstraintName("FK_Product_Materail1");
                });

            modelBuilder.Entity("Gryffindor.Models.PurchaseRequest", b =>
                {
                    b.HasOne("Gryffindor.Models.Customer", "Customer")
                        .WithMany("PurchaseRequest")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Purchase_Request_Customer");

                    b.HasOne("Gryffindor.Models.DateYearoff", "Date")
                        .WithMany("PurchaseRequest")
                        .HasForeignKey("DateId")
                        .HasConstraintName("FK_Purchase_Request_Date_yearoff");

                    b.HasOne("Gryffindor.Models.Issue", "Issue")
                        .WithMany("PurchaseRequest")
                        .HasForeignKey("IssueId")
                        .HasConstraintName("FK_Purchase_Request_Issue");

                    b.HasOne("Gryffindor.Models.Machine", "Machine")
                        .WithMany("PurchaseRequest")
                        .HasForeignKey("MachineId")
                        .HasConstraintName("FK_Purchase_Request_Machine");

                    b.HasOne("Gryffindor.Models.Order", "Order")
                        .WithMany("PurchaseRequest")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_Purchase_Request_Order");

                    b.HasOne("Gryffindor.Models.ProductStatus", "Status")
                        .WithMany("PurchaseRequest")
                        .HasForeignKey("StatusId")
                        .HasConstraintName("FK_Purchase_Request_Product_status");
                });

            modelBuilder.Entity("Gryffindor.Models.SchedulePlan", b =>
                {
                    b.HasOne("Gryffindor.Models.DateMount", "Month")
                        .WithMany("SchedulePlan")
                        .HasForeignKey("MonthId")
                        .HasConstraintName("FK_Schedule_plan_Date_Mount");

                    b.HasOne("Gryffindor.Models.PurchaseRequest", "Purchase")
                        .WithMany("SchedulePlan")
                        .HasForeignKey("PurchaseId")
                        .HasConstraintName("FK_Schedule_plan_Purchase_Request");
                });
#pragma warning restore 612, 618
        }
    }
}
