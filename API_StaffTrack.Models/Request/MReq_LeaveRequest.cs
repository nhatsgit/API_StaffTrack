using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_StaffTrack.Models.Request
{
    public class MReq_LeaveRequest
    {
        [JsonIgnore]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime LeaveDate { get; set; }

        [StringLength(255)]
        public string? Reason { get; set; }
        [JsonIgnore]
        public int? ApprovedBy { get; set; }
        [JsonIgnore]
        public DateTime? ApprovalDate { get; set; }

        public int? Status { get; set; }
    }

}
