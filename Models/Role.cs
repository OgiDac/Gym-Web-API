using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeretanaWebApi.Models
{
    public class Role
    {
        private int roleId;
        private string roleName;

        public Role(int roleId, string roleName)
        {
            this.RoleId = roleId;
            this.RoleName = roleName;
        }

        public int RoleId { get => roleId; set => roleId = value; }
        public string RoleName { get => roleName; set => roleName = value; }
    }
}