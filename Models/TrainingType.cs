using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeretanaWebApi.Models
{
    public class TrainingType
    {
        private int typeId;
        private string trainingName;

        public TrainingType(int typeId, string trainingName)
        {
            this.TypeId = typeId;
            this.TrainingName = trainingName;
        }

        public int TypeId { get => typeId; set => typeId = value; }
        public string TrainingName { get => trainingName; set => trainingName = value; }
    }
}