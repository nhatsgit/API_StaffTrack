using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_StaffTrack.Models.Response
{
    public class MRes_Notification
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string? NotificationType { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? SentTime { get; set; }
        public DateTime? ReadTime { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

       
    }
}
