using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_StaffTrack.Data.Entities;

[Index("Email", Name = "UQ__Employee__AB6E6164AFFE5A4A", IsUnique = true)]
public partial class Employee
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("phone_number")]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [Column("department")]
    [StringLength(100)]
    public string? Department { get; set; }

    [Column("position")]
    [StringLength(100)]
    public string? Position { get; set; }

    [Column("join_date")]
    public DateOnly? JoinDate { get; set; }

    [Column("is_active")]
    public bool? IsActive { get; set; }

    [Column("status")]
    public int? Status { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("update_at", TypeName = "datetime")]
    public DateTime? UpdateAt { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();

    [InverseProperty("ApprovedByNavigation")]
    public virtual ICollection<LeaveRequest> LeaveRequestApprovedByNavigations { get; set; } = new List<LeaveRequest>();

    [InverseProperty("Employee")]
    public virtual ICollection<LeaveRequest> LeaveRequestEmployees { get; set; } = new List<LeaveRequest>();

    [InverseProperty("Employee")]
    public virtual ICollection<MonthlyReport> MonthlyReports { get; set; } = new List<MonthlyReport>();

    [InverseProperty("Employee")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [InverseProperty("Employee")]
    public virtual ICollection<WorkPlan> WorkPlans { get; set; } = new List<WorkPlan>();
}
