/*
        Date: 05/06/2020                              
        Author: Cesar Sergio Martinez Palacios
 */

using BusinessDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IAdmninistratorDAO
    {
        Administrator GetAdministratorByUser(string adminUsername);
    }
}
