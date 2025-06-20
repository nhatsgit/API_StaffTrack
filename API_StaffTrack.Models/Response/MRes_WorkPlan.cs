using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_StaffTrack.Models.Response
{
    public class MRes_WorkPlan
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = null!;

        public DateTime WorkDate { get; set; }

        public string TaskDescription { get; set; } = null!;

        public string? ProgressNote { get; set; }

        public string? Note { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }
    }

}
