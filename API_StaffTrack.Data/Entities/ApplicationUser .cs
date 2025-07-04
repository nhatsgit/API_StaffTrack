﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_StaffTrack.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
       
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
