using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_StaffTrack.Models.Response
{
    public class MRes_AttendanceRecord
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public TimeSpan? CheckInTime { get; set; }

        public double? CheckInLat { get; set; }

        public double? CheckInLng { get; set; }

        public TimeSpan? CheckOutTime { get; set; }

        public double? CheckOutLat { get; set; }

        public double? CheckOutLng { get; set; }

        public int? WorkCompletionPercent { get; set; }

        public string? Note { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }
    }

}
