using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeretanaWebApi.Models
{
    public class User
    {
        private int roleId;
        private string fitnessCenter;
        private string username;
        private string password;

        public User(int roleId, string username, string password, string fitnessCenter)
        {
            this.roleId = roleId;
            this.fitnessCenter = fitnessCenter;
            this.Username = username;
            this.Password = password;
        }

        public int RoleId { get => roleId; set => roleId = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string FitnessCenter { get => fitnessCenter; set => fitnessCenter = value; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        
    }
}