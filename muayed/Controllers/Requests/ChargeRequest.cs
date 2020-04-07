using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Controllers.Requests
{
    public class ChargeRequest
    {
        public string UserName { get; set; }
        public string phoneNumber { get; set; }
        public int balance { get; set; }
        public string jawwalCompanyBalance { get; set; }
    }
}
