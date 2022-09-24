using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeretanaWebApi.Models
{
    public class Comment
    {
        private string visitor;
        private string fitnessCenter;
        private string commentsTxt;
        private int raiting;
        private int status;

        public Comment(string visitor,int status, string fitnessCenter, string commentsTxt, int raiting)
        {
            this.Visitor = visitor;
            this.FitnessCenter = fitnessCenter;
            this.CommentsTxt = commentsTxt;
            this.Raiting = raiting;
            this.Status = status;
        }

        public int Status { get => status; set => status = value; }
        public string Visitor { get => visitor; set => visitor = value; }
        public string FitnessCenter { get => fitnessCenter; set => fitnessCenter = value; }
        public string CommentsTxt { get => commentsTxt; set => commentsTxt = value; }
        public int Raiting { get => raiting; set => raiting = value; }
    }
}