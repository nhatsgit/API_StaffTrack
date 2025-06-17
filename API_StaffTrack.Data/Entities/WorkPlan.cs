using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_StaffTrack.Data.Entities;

[Index("EmployeeId", "WorkDate", Name = "UQ_WorkPlan_Unique", IsUnique = true)]
public partial class WorkPlan
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [Column("work_date")]
    public DateOnly WorkDate { get; set; }

    [Column("task_description")]
    [StringLength(1000)]
    public string TaskDescription { get; set; } = null!;

    [Column("progress_note")]
    [StringLength(1000)]
    public string? ProgressNote { get; set; }

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
    [InverseProperty("WorkPlans")]
    public virtual Employee Employee { get; set; } = null!;
}
