using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_StaffTrack.Models.Request
{
    public class MReq_AttendanceRecord
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime AttendanceDate { get; set; }

        public TimeSpan? CheckInTime { get; set; }

        public double? CheckInLat { get; set; }

        public double? CheckInLng { get; set; }

        public TimeSpan? CheckOutTime { get; set; }

        public double? CheckOutLat { get; set; }

        public double? CheckOutLng { get; set; }

        [Range(0, 100)]
        public int? WorkCompletionPercent { get; set; }

        [StringLength(255)]
        public string? Note { get; set; }

        public int? Status { get; set; }
    }

}
