using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_StaffTrack.Data.Entities;

[Index("EmployeeId", "AttendanceDate", Name = "UQ_Employee_Date", IsUnique = true)]
public partial class AttendanceRecord
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [Column("attendance_date")]
    public DateOnly AttendanceDate { get; set; }

    [Column("check_in_time")]
    public TimeOnly? CheckInTime { get; set; }

    [Column("check_in_lat")]
    public double? CheckInLat { get; set; }

    [Column("check_in_lng")]
    public double? CheckInLng { get; set; }

    [Column("check_out_time")]
    public TimeOnly? CheckOutTime { get; set; }

    [Column("check_out_lat")]
    public double? CheckOutLat { get; set; }

    [Column("check_out_lng")]
    public double? CheckOutLng { get; set; }

    [Column("work_completion_percent")]
    public int? WorkCompletionPercent { get; set; }

    [Column("note")]
    [StringLength(255)]
    public string? Note { get; set; }

    [Column("status")]
    public int? Status { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("update_at", TypeName = "datetime")]
    public DateTime? UpdateAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("AttendanceRecords")]
    public virtual Employee Employee { get; set; } = null!;
}
