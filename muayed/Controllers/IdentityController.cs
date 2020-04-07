using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myProject.Controllers.Requests;
using myProject.DAL.Interface;
using myProject.Model;

namespace myProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUserInfoDAL _userInfoDAL;

        public IdentityController(IUserInfoDAL userInfoDAL)
        {
            _userInfoDAL = userInfoDAL;
        }


        // GET: api/Identity
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userInfoDAL.GetUsers();
            
            if (users == null || users.Count < 1)
            {
                return NotFound();
            }

            return Ok(users);
        }

        //Post api/Identity/Login
        [HttpPost("Login")]
        public IActionResult CheckLogin([FromBody] LoginRequest loginRequest)
        {
            User isAuth = _userInfoDAL.CheckLogin(loginRequest);
          

            if (isAuth == null)
            {
                return Unauthorized();
            }

            return Ok(new {success=true , balance = isAuth.balance  });
        }


        //Put api/Identity/Transfer
        [HttpPut("Transfer")]
        public IActionResult TransferRequest(int id, [FromBody] TransferRequest transferRequest)
        {
            bool isbalance = _userInfoDAL.CheckBalance(transferRequest);
            bool isTransfer = _userInfoDAL.Transfer(transferRequest);


            if (isTransfer == false || isbalance == false)
            {
                return Unauthorized();
            }
            return Ok(new { success = true });
        }

        //Put api/Identity/Charge
        [HttpPut("Charge")]
        public IActionResult ChargeRequest(int id, [FromBody] ChargeRequest chargeRequest)
        {
            int balance = _userInfoDAL.Charge(chargeRequest);
          //  bool isTransfer = _userInfoDAL.Transfer(transferRequest);


            if (balance < 0)
            {
                return Unauthorized();
            }
            else
            {
                
                //isbalance = isbalance - chargeRequest.balance;
                

            }
            return Ok(new { success = true , balance });
        }



        // GET: api/Identity/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Identity
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Identity/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
