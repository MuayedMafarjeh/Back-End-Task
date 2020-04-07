using myProject.Controllers.Requests;
using myProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.DAL.Interface
{
    public interface IUserInfoDAL
    {
        List<User> GetUsers();
        User CheckLogin(LoginRequest loginRequest);
        bool Transfer(TransferRequest transfer);

        bool CheckBalance(TransferRequest transfer);

        int Charge(ChargeRequest chargeRequest);
    }
}
