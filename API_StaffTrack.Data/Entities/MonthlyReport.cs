using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_StaffTrack.Data.Entities;

public partial class MonthlyReport
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [Column("year")]
    public int Year { get; set; }

    [Column("month")]
    public int Month { get; set; }

    [Column("days_present")]
    public int? DaysPresent { get; set; }

    [Column("days_absent")]
    public int? DaysAbsent { get; set; }

    [Column("days_leave")]
    public int? DaysLeave { get; set; }

    [Column("late_count")]
    public int? LateCount { get; set; }

    [Column("status")]
    public int? Status { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("update_at", TypeName = "datetime")]
    public DateTime? UpdateAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("MonthlyReports")]
    public virtual Employee Employee { get; set; } = null!;
}
