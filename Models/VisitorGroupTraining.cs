using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeretanaWebApi.Models
{
    public class VisitorGroupTraining
    {
        private bool? deleted;
        private string username;
        private string tariningId;

        public VisitorGroupTraining(string username, bool? deleted, string tariningId)
        {
            
            this.Deleted = deleted;
            this.Username = username;
            this.TariningId = tariningId;
        }

        public bool? Deleted { get => deleted; set => deleted = value; }
        public string Username { get => username; set => username = value; }
        public string TariningId { get => tariningId; set => tariningId = value; }
    }
}