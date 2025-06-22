using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_StaffTrack.Models.Request
{
    public class MReq_MonthlyReport
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Month { get; set; }

        [Range(0, 31)]
        public int? DaysPresent { get; set; }

        [Range(0, 31)]
        public int? DaysAbsent { get; set; }

        [Range(0, 31)]
        public int? DaysLeave { get; set; }

        [Range(0, int.MaxValue)]
        public int? LateCount { get; set; }

        public int? Status { get; set; }
    }

}
