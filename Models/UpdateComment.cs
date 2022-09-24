using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeretanaWebApi.Models
{
    public class UpdateComment
    {
        private string visitor;
        private string fitnessCenter;
        private int status;

        public UpdateComment(string visitor, string fitnessCenter, int status)
        {
            Visitor = visitor;
            FitnessCenter = fitnessCenter;
            Status = status;
        }

        public int Status { get => status; set => status = value; }
        public string Visitor { get => visitor; set => visitor = value; }
        public string FitnessCenter { get => fitnessCenter; set => fitnessCenter = value; }
    }
}