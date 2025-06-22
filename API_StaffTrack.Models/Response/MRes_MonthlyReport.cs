using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_StaffTrack.Models.Response
{
    public class MRes_MonthlyReport
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int? DaysPresent { get; set; }

        public int? DaysAbsent { get; set; }

        public int? DaysLeave { get; set; }

        public int? LateCount { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }
    }

}
