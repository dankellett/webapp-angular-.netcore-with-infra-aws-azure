using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_template.Models
{
    public class UserOrgReport
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ReportsToUserId { get; set; }
    }
}
