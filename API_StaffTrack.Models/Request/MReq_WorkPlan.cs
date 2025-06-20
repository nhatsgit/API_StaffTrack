using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_StaffTrack.Models.Request
{
    public class MReq_WorkPlan
    {
        public int? Id { get; set; }  

        public int EmployeeId { get; set; }

        public DateTime WorkDate { get; set; }

        public string TaskDescription { get; set; } = null!;

        public string? ProgressNote { get; set; }

        public string? Note { get; set; }

        public int? Status { get; set; }
    }

}
