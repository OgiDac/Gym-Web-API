using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeretanaWebApi.Models
{
    public class EditUser
    {
        private bool blocked;
        private string username;
        private string password;
        private string name;
        private string surname;
        private string gender;
        private string email;
        private string birthDate;
        private int roleId;

        public EditUser(string username, bool blocked, string password, string name, string surname, string gender, string email, string birthDate, int roleId)
        {
            this.Blocked = blocked;
            this.Username = username;
            this.Password = password;
            this.Name = name;
            this.Surname = surname;
            this.Gender = gender;
            this.Email = email;
            this.BirthDate = birthDate;
            this.RoleId = roleId;
        }

        public string Username { get => username; set => username = value; }
        public bool Blocked { get => blocked; set => blocked = value; }
        public string Password { get => password; set => password = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Email { get => email; set => email = value; }
        public string BirthDate { get => birthDate; set => birthDate = value; }
        public int RoleId { get => roleId; set => roleId = value; }
    }
}