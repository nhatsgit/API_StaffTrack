using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_StaffTrack.Data.Entities;

public partial class Notification
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [Column("title")]
    [StringLength(200)]
    public string Title { get; set; } = null!;

    [Column("message")]
    public string Message { get; set; } = null!;

    [Column("notification_type")]
    [StringLength(50)]
    public string? NotificationType { get; set; }

    [Column("is_read")]
    public bool? IsRead { get; set; }

    [Column("sent_time", TypeName = "datetime")]
    public DateTime? SentTime { get; set; }

    [Column("read_time", TypeName = "datetime")]
    public DateTime? ReadTime { get; set; }

    [Column("status")]
    public int? Status { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("update_at", TypeName = "datetime")]
    public DateTime? UpdateAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Notifications")]
    public virtual Employee Employee { get; set; } = null!;
}
