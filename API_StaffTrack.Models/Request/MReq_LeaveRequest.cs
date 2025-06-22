using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_StaffTrack.Models.Request
{
    public class MReq_LeaveRequest
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime LeaveDate { get; set; }

        [StringLength(255)]
        public string? Reason { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public int? Status { get; set; }
    }

}
