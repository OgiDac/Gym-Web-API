using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeretanaWebApi.Models
{
    public class NewUser
    {
        private string username;
        private string password;
        private bool blocked;
        private string name;
        private string surname;
        private string gender;
        private string email;
        private string birthDate;
        private int roleId;
        bool isLogged = false;
        private List<string> visitorTrainings = new List<string>();
        private List<string> trainerTrainings = new List<string>();
        private string fitnessCenterTrainer;
        private string fitnessCenterOwner;

        public NewUser(string username, bool blocked, string password, string name, string surname, string gender, string email, string birthDate, int roleId, bool isLogged, List<string> visitorTrainings, List<string> trainerTrainings, string fitnessCenterTrainer, string fitnessCenterOwner)
        {
            this.Username = username;
            this.Password = password;
            this.Name = name;
            this.Surname = surname;
            this.Gender = gender;
            this.Email = email;
            this.BirthDate = birthDate;
            this.RoleId = roleId;
            this.IsLogged = isLogged;
            this.VisitorTrainings = visitorTrainings;
            this.TrainerTrainings = trainerTrainings;
            this.FitnessCenterTrainer = fitnessCenterTrainer;
            this.FitnessCenterOwner = fitnessCenterOwner;
            this.Blocked = blocked;
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
        public bool IsLogged { get => isLogged; set => isLogged = value; }
        public List<string> VisitorTrainings { get => visitorTrainings; set => visitorTrainings = value; }
        public List<string> TrainerTrainings { get => trainerTrainings; set => trainerTrainings = value; }
        public string FitnessCenterTrainer { get => fitnessCenterTrainer; set => fitnessCenterTrainer = value; }
        public string FitnessCenterOwner { get => fitnessCenterOwner; set => fitnessCenterOwner = value; }
 

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }


    }
}