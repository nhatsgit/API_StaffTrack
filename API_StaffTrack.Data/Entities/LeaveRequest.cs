using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_StaffTrack.Data.Entities;

public partial class LeaveRequest
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [Column("leave_date")]
    public DateOnly LeaveDate { get; set; }

    [Column("reason")]
    [StringLength(255)]
    public string? Reason { get; set; }

    [Column("request_date", TypeName = "datetime")]
    public DateTime? RequestDate { get; set; }

    [Column("approved_by")]
    public int? ApprovedBy { get; set; }

    [Column("approval_date", TypeName = "datetime")]
    public DateTime? ApprovalDate { get; set; }

    [Column("status")]
    public int? Status { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("update_at", TypeName = "datetime")]
    public DateTime? UpdateAt { get; set; }

    [ForeignKey("ApprovedBy")]
    [InverseProperty("LeaveRequestApprovedByNavigations")]
    public virtual Employee? ApprovedByNavigation { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("LeaveRequestEmployees")]
    public virtual Employee Employee { get; set; } = null!;
}
