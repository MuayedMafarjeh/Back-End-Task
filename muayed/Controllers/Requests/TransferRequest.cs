using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Controllers.Requests
{
    public class TransferRequest
    {
        public string UserName { get; set; }
        public string phoneNumber { get; set; }
        public int balance { get; set; }
    }
  
}
