using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeretanaWebApi.Models
{
    public class EditTraining
    {
        private string id;
        private string trainingName;
        private string trainerUsername;
        private string fitnessCenter;
        private string trainingType;
        private int trainingDuration;
        private string dateAndTime;
        private int maxNumberOfPeople;

        public EditTraining(string id, string trainingName, string trainerUsername, string fitnessCenter, string trainingType, int trainingDuration, string dateAndTime, int maxNumberOfPeople)
        {
            this.Id = id;
            this.TrainingName = trainingName;
            this.TrainerUsername = trainerUsername;
            this.FitnessCenter = fitnessCenter;
            this.TrainingType = trainingType;
            this.TrainingDuration = trainingDuration;
            this.DateAndTime = dateAndTime;
            this.MaxNumberOfPeople = maxNumberOfPeople;
        }

        public string Id { get => id; set => id = value; }
        public string TrainingName { get => trainingName; set => trainingName = value; }
        public string TrainerUsername { get => trainerUsername; set => trainerUsername = value; }
        public string FitnessCenter { get => fitnessCenter; set => fitnessCenter = value; }
        public string TrainingType { get => trainingType; set => trainingType = value; }
        public int TrainingDuration { get => trainingDuration; set => trainingDuration = value; }
        public string DateAndTime { get => dateAndTime; set => dateAndTime = value; }
        public int MaxNumberOfPeople { get => maxNumberOfPeople; set => maxNumberOfPeople = value; }
    }
}