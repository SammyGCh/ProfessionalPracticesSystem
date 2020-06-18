/*
        Date: 05/06/2020                               
        Author: Cesar Sergio Martinez Palacios
 */
 using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BusinessDomain
{
    public class Administrator
    {
        private int idAdministrator;
        private string username;
        private string password;

        public int IdAdministrator
        {
            get => idAdministrator;
            set => idAdministrator = value;
        }

        public string Username
        {
            get => username;
            set => username = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }
    }
}
