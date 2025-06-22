using System;
using System.Collections.Generic;
using API_StaffTrack.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_StaffTrack.Data.EF;

public partial class MainDbContext :  IdentityDbContext<ApplicationUser>
{
    public MainDbContext()
    {
    }

    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AttendanceRecord> AttendanceRecords { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<MonthlyReport> MonthlyReports { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<WorkPlan> WorkPlans { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-NEDCV2T\\SQLEXPRESS;Initial Catalog=DB2;Integrated Security=True;TrustServerCertificate=True;");
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    => optionsBuilder.UseSqlServer("Data Source=LAPTOP-LJH881OS;Initial Catalog=DB2;Integrated Security=True;TrustServerCertificate=True;");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AttendanceRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attendan__3213E83F10196D76");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(1);
            entity.Property(e => e.WorkCompletionPercent).HasDefaultValue(0);

            entity.HasOne(d => d.Employee).WithMany(p => p.AttendanceRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attendance_Employee");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83F487E3A38");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Status).HasDefaultValue(1);
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LeaveReq__3213E83F2BB70371");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RequestDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.LeaveRequestApprovedByNavigations).HasConstraintName("FK_LeaveRequest_Approver");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveRequestEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveRequest_Employee");
        });

        modelBuilder.Entity<MonthlyReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MonthlyR__3213E83FDE8167B3");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DaysAbsent).HasDefaultValue(0);
            entity.Property(e => e.DaysLeave).HasDefaultValue(0);
            entity.Property(e => e.DaysPresent).HasDefaultValue(0);
            entity.Property(e => e.LateCount).HasDefaultValue(0);
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.Employee).WithMany(p => p.MonthlyReports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Report_Employee");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3213E83F93E970CF");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.SentTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.Employee).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notification_Employee");
        });

        modelBuilder.Entity<WorkPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WorkPlan__3213E83F452F106B");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.Employee).WithMany(p => p.WorkPlans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkPlan_Employee");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
