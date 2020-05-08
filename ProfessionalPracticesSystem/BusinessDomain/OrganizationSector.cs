/*
    Date: 06/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;

namespace BusinessDomain
{
    public class OrganizationSector
    {
        private int idOrganizationSector;
        private String name;
        private int status;

        public int IdOrganizationSector
        {
            get => idOrganizationSector;
            set => idOrganizationSector = value;
        }

        public String Name
        {
            get => name;
            set => name = value;
        }

        public int Status
        {
            get => status;
            set => status = value;
        }
    }
}
