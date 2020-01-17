using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Gryffindor.Models
{
    public partial class OrderProjectContext : DbContext
    {
        public OrderProjectContext()
        {
        }

        public OrderProjectContext(DbContextOptions<OrderProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<DateMount> DateMount { get; set; }
        public virtual DbSet<DateYearoff> DateYearoff { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Issue> Issue { get; set; }
        public virtual DbSet<Machine> Machine { get; set; }
        public virtual DbSet<Materail> Materail { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductStatus> ProductStatus { get; set; }
        public virtual DbSet<PurchaseRequest> PurchaseRequest { get; set; }
        public virtual DbSet<SchedulePlan> SchedulePlan { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=NTB-118;Database=Order Project;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .HasColumnName("Customer_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomerAddress)
                    .HasColumnName("Customer_Address")
                    .IsUnicode(false);

                entity.Property(e => e.CustomerCompany)
                    .HasColumnName("Customer_Company")
                    .IsUnicode(false);

                entity.Property(e => e.CustomerTel)
                    .HasColumnName("Customer_Tel")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DateMount>(entity =>
            {
                entity.HasKey(e => e.MonthId);

                entity.ToTable("Date_Mount");

                entity.Property(e => e.MonthId)
                    .HasColumnName("Month_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.MonthName)
                    .HasColumnName("Month_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DateYearoff>(entity =>
            {
                entity.HasKey(e => e.DateId);

                entity.ToTable("Date_yearoff");

                entity.Property(e => e.DateId)
                    .HasColumnName("Date_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateDateoff)
                    .HasColumnName("Date_dateoff")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId)
                    .HasColumnName("Employee_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmployeeDepartment)
                    .HasColumnName("Employee_Department")
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeName)
                    .HasColumnName("Employee_Name")
                    .IsUnicode(false);

                entity.Property(e => e.EmployeePosition)
                    .HasColumnName("Employee_Position")
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeSaraly).HasColumnName("Employee_Saraly");

                entity.Property(e => e.EmployeeSex)
                    .HasColumnName("Employee_Sex")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.Property(e => e.IssueId)
                    .HasColumnName("issue_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmployeeName)
                    .HasColumnName("Employee_name")
                    .IsUnicode(false);

                entity.Property(e => e.IssueName)
                    .HasColumnName("issue_name")
                    .IsUnicode(false);

                entity.Property(e => e.IssueStatus)
                    .HasColumnName("issue_status")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.Property(e => e.MachineId)
                    .HasColumnName("Machine_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.MachineName)
                    .HasColumnName("Machine_Name")
                    .IsUnicode(false);

                entity.Property(e => e.MachineQty).HasColumnName("Machine_QTY");

                entity.Property(e => e.MachineTime).HasColumnName("Machine_Time");
            });

            modelBuilder.Entity<Materail>(entity =>
            {
                entity.Property(e => e.MaterailId)
                    .HasColumnName("Materail_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.MaterailColor)
                    .HasColumnName("Materail_Color")
                    .IsUnicode(false);

                entity.Property(e => e.MaterailName)
                    .HasColumnName("Materail_Name")
                    .IsUnicode(false);

                entity.Property(e => e.MaterailQty).HasColumnName("Materail_QTY");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .HasColumnName("Order_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomerCompany)
                    .HasColumnName("Customer_Company")
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.EmployeeDepartment)
                    .HasColumnName("Employee_Department")
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.EmployeeName)
                    .HasColumnName("Employee_Name")
                    .IsUnicode(false);

                entity.Property(e => e.MaterailColor)
                    .HasColumnName("Materail_Color")
                    .IsUnicode(false);

                entity.Property(e => e.MaterailId).HasColumnName("Materail_ID");

                entity.Property(e => e.MaterailName)
                    .HasColumnName("Materail_Name")
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate)
                    .HasColumnName("Order_Date")
                    .HasColumnType("date");

                entity.Property(e => e.OrderQty).HasColumnName("Order_QTY");

                entity.Property(e => e.ProductColor)
                    .HasColumnName("Product_Color")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("Product_ID");

                entity.Property(e => e.ProductName)
                    .HasColumnName("Product_Name")
                    .IsUnicode(false);

                entity.Property(e => e.ProductSize).HasColumnName("Product_Size");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Order_Customer");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Order_Employee");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Order_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId)
                    .HasColumnName("Product_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.MarerailName)
                    .HasColumnName("Marerail_Name")
                    .IsUnicode(false);

                entity.Property(e => e.MaterailId).HasColumnName("Materail_ID");

                entity.Property(e => e.ProductColor)
                    .HasColumnName("Product_Color")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasColumnName("Product_Name")
                    .IsUnicode(false);

                entity.Property(e => e.ProductSize)
                    .HasColumnName("Product_Size")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Materail)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.MaterailId)
                    .HasConstraintName("FK_Product_Materail1");
            });

            modelBuilder.Entity<ProductStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("Product_status");

                entity.Property(e => e.StatusId)
                    .HasColumnName("Status_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.StatusName)
                    .HasColumnName("Status_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PurchaseRequest>(entity =>
            {
                entity.HasKey(e => e.PurchaseId);

                entity.ToTable("Purchase_Request");

                entity.Property(e => e.PurchaseId)
                    .HasColumnName("Purchase_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomerCompany)
                    .HasColumnName("Customer_Company")
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.DateDateoff)
                    .HasColumnName("Date_dateoff")
                    .HasColumnType("date");

                entity.Property(e => e.DateId).HasColumnName("Date_ID");

                entity.Property(e => e.IssueId).HasColumnName("issue_id");

                entity.Property(e => e.IssueName)
                    .HasColumnName("issue_name")
                    .IsUnicode(false);

                entity.Property(e => e.MachineId).HasColumnName("Machine_ID");

                entity.Property(e => e.MachineName)
                    .HasColumnName("Machine_Name")
                    .IsUnicode(false);

                entity.Property(e => e.MachineTime)
                    .HasColumnName("Machine_Time")
                    .HasColumnType("datetime");

                entity.Property(e => e.MaterailId).HasColumnName("Materail_ID");

                entity.Property(e => e.MaterailName)
                    .HasColumnName("Materail_name")
                    .IsUnicode(false);

                entity.Property(e => e.MonthId).HasColumnName("Month_ID");

                entity.Property(e => e.MonthName)
                    .HasColumnName("Month_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                entity.Property(e => e.OrderQty).HasColumnName("Order_QTY");

                entity.Property(e => e.PhurchaseDate)
                    .HasColumnName("Phurchase_Date")
                    .HasColumnType("date");

                entity.Property(e => e.PhurchseDoc)
                    .HasColumnName("Phurchse_Doc")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnName("Product_ID");

                entity.Property(e => e.ProductName)
                    .HasColumnName("Product_Name")
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseTotal).HasColumnName("Purchase_Total");

                entity.Property(e => e.StatusId).HasColumnName("Status_ID");

                entity.Property(e => e.StatusName)
                    .HasColumnName("Status_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.PurchaseRequest)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Purchase_Request_Customer");

                entity.HasOne(d => d.Date)
                    .WithMany(p => p.PurchaseRequest)
                    .HasForeignKey(d => d.DateId)
                    .HasConstraintName("FK_Purchase_Request_Date_yearoff");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.PurchaseRequest)
                    .HasForeignKey(d => d.IssueId)
                    .HasConstraintName("FK_Purchase_Request_Issue");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.PurchaseRequest)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("FK_Purchase_Request_Machine");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.PurchaseRequest)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Purchase_Request_Order");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.PurchaseRequest)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Purchase_Request_Product_status");
            });

            modelBuilder.Entity<SchedulePlan>(entity =>
            {
                entity.HasKey(e => e.ScheduleId);

                entity.ToTable("Schedule_plan");

                entity.Property(e => e.ScheduleId)
                    .HasColumnName("Schedule_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.MonthId).HasColumnName("Month_ID");

                entity.Property(e => e.MonthName)
                    .HasColumnName("Month_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseId).HasColumnName("Purchase_ID");

                entity.Property(e => e.SheduleTotal).HasColumnName("Shedule_Total");

                entity.HasOne(d => d.Month)
                    .WithMany(p => p.SchedulePlan)
                    .HasForeignKey(d => d.MonthId)
                    .HasConstraintName("FK_Schedule_plan_Date_Mount");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.SchedulePlan)
                    .HasForeignKey(d => d.PurchaseId)
                    .HasConstraintName("FK_Schedule_plan_Purchase_Request");
            });
        }
    }
}
