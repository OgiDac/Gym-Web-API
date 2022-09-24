using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeretanaWebApi.Models
{

    public class GroupTraining
    {
        private string id;
        private string trainingName;
        private string trainerUsername;
        private bool deleted;
        private string fitnessCenter;
        private string trainingType;
        private int trainingDuration;
        private string dateAndTime;
        private int maxNumberOfPeople;
        private List<String> visitorList = new List<String>();

        public GroupTraining(string id, string trainerUsername, bool? deleted, string trainingName, string fitnessCenter, string trainingType, int trainingDuration, string dateAndTime, int maxNumberOfPeople, List<String> visitorList)
        {
            if (id != null && id.Length > 0)
            {
                this.id = id;
            }
            else
            {
                GenerateId();
            }

            if (deleted != null)
            {
                this.deleted = (bool)deleted;
            }
            else { this.deleted = false; }

            this.TrainingName = trainingName;
            this.TrainerUsername = trainerUsername;
            this.FitnessCenter = fitnessCenter;
            this.TrainingType = trainingType;
            this.TrainingDuration = trainingDuration;
            this.DateAndTime = dateAndTime;
            this.MaxNumberOfPeople = maxNumberOfPeople;
            this.VisitorList = visitorList;
        }

        public string Id { get => id; set => id = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
        public string TrainingName { get => trainingName; set => trainingName = value; }
        public string TrainerUsername { get => trainerUsername; set => trainerUsername = value; }
        public string FitnessCenter { get => fitnessCenter; set => fitnessCenter = value; }
        public string TrainingType { get => trainingType; set => trainingType = value; }
        public int TrainingDuration { get => trainingDuration; set => trainingDuration = value; }
        public string DateAndTime { get => dateAndTime; set => dateAndTime = value; }
        public int MaxNumberOfPeople { get => maxNumberOfPeople; set => maxNumberOfPeople = value; }
        public List<String> VisitorList { get => visitorList; set => visitorList = value; }


        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        private void GenerateId()
        {
            this.Id = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();

        }

    }


}