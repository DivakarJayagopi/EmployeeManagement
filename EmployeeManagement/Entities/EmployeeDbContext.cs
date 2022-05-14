using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EmployeeManagement.Entities
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext() : base("ASPEntities")
        {
        }
        public DbSet<Chats> Chats { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<LeaveRequest> LeaveRequest { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsConnector> NewsConnector { get; set; }
        public DbSet<PaySlip> PaySlip { get; set; }
        public DbSet<EmployeeSalaryInfo> EmployeeSalaryInfo { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chats>().ToTable("Chats");
            modelBuilder.Entity<Employees>().ToTable("Employees");
            modelBuilder.Entity<LeaveRequest>().ToTable("LeaveRequest");
            modelBuilder.Entity<News>().ToTable("News");
            modelBuilder.Entity<NewsConnector>().ToTable("NewsConnector");
            modelBuilder.Entity<PaySlip>().ToTable("PaySlip");
            modelBuilder.Entity<EmployeeSalaryInfo>().ToTable("EmployeeSalaryInfo");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}