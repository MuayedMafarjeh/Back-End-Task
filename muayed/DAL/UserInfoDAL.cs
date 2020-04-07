using Dapper;
using Microsoft.Extensions.Configuration;
using myProject.Controllers.Requests;
using myProject.DAL.Interface;
using myProject.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace myProject.DAL
{
    public class UserInfoDAL : IUserInfoDAL
    {
        private readonly IConfiguration _configuration;

        public UserInfoDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<User> GetUsers()
        {
            List<User> users = null;
            try
            {
                //_configuration.GetConnectionString("DB")
                using (MySqlConnection sql = new MySqlConnection("Server = localhost; Port = 3306; Database = usersdb; Uid = Muayed; Pwd = Muayed123;"))
                {
                    users = sql.Query<User>("SELECT * FROM usersdb.userinfo ").ToList();
                   
                }

            }
            catch (System.Exception ex)
            {

            }
            return users;

        }

        public User CheckLogin(LoginRequest loginRequest)
        {
            bool success = false;
            User user = null;

            string sqlQuery = "SELECT * FROM usersdb.userinfo WHERE userName = @UserName AND password = @password";

            try
            {

                using (MySqlConnection sql = new MySqlConnection(_configuration.GetConnectionString("DB")))
                {
                    user = sql.Query<User>(sqlQuery, new { loginRequest.password, loginRequest.UserName }).First();
                 
                        success = true;

                }

            }
            catch (System.Exception ex)
            {
                throw new System.Exception("userName or Password not correct ");
              
            }
            return user;

        }

        public bool Transfer(TransferRequest transferRequest)
        {
            bool success = false;

            string sqlQuery = "SELECT balance FROM usersdb.userinfo WHERE  phoneNumber = @phoneNumber";

            try
            {

                using (MySqlConnection sql = new MySqlConnection(_configuration.GetConnectionString("DB")))
                {
                    int balance = sql.Query<int>(sqlQuery, new { transferRequest.phoneNumber }).FirstOrDefault();
                    balance += transferRequest.balance;
                    if (balance > 0)
                    {
                        string balanceQuery = "UPDATE  usersdb.userinfo SET balance=@balance WHERE  phoneNumber = @phoneNumber";

                        int isbalance = sql.Query<int>(balanceQuery, new { balance, transferRequest.phoneNumber }).FirstOrDefault();
                        success = true;

                    }
                      

                }

            }
            catch (System.Exception ex)
            {

            }
            return success;

        }

        public bool  CheckBalance(TransferRequest transferRequest)
        {
            bool success = false;
            int value = 0;

            string sqlQuery = "SELECT balance FROM usersdb.userinfo WHERE  userName = @UserName";

            try
            {

                using (MySqlConnection sql = new MySqlConnection(_configuration.GetConnectionString("DB")))
                {
                    int balance = sql.Query<int>(sqlQuery, new { transferRequest.UserName }).FirstOrDefault();
               
                    if (balance > transferRequest.balance)
                    {
                        balance = balance - transferRequest.balance;
                        string balanceQuery = "UPDATE  usersdb.userinfo SET balance=@balance WHERE  userName = @UserName";

                           int isbalance = sql.Query<int>(balanceQuery, new { balance ,transferRequest.UserName }).FirstOrDefault();
                            value = isbalance;
                        success = true;
                    }
                   

                }

            }
            catch (System.Exception ex)
            {

            }
            return success;

        }

        public int Charge(ChargeRequest chargeRequest)
        {
            bool success = false;
            int value = 0;
            User charge = null;
            string sqlQuery = "SELECT balance, jawwalCompanyBalance FROM usersdb.userinfo WHERE   phoneNumber = @phoneNumber";

            try
            {

                using (MySqlConnection sql = new MySqlConnection(_configuration.GetConnectionString("DB")))
                {
                    charge = sql.Query<User>(sqlQuery, new { chargeRequest.phoneNumber}).FirstOrDefault();

                 //  string v = charge.Select(t => t.balance).Distinct().ToString();
                    //  balance += transferRequest.balance;
                    if (charge.balance > chargeRequest.balance)
                    {
                        charge.balance = charge.balance - chargeRequest.balance;

                        charge.jawwalCompanyBalance += chargeRequest.balance; 

                 string balanceQuery = "UPDATE  usersdb.userinfo SET balance=@balance, jawwalCompanyBalance=@jawwalCompanyBalance WHERE  phoneNumber = @phoneNumber";
                        int isbalance = sql.Query<int>(balanceQuery, new { charge.balance, charge.jawwalCompanyBalance, chargeRequest.phoneNumber }).FirstOrDefault();

                       
                        value = charge.balance;
                        success = true;
                 

                    }


                }

            }
            catch (System.Exception ex)
            {

            }
            return value;

        }

    
    }
}
