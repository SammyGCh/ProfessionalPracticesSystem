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
        private const int UNDEFINED = 0;
        private const String UNKNOWN = "";

        public OrganizationSector()
        {
            idOrganizationSector = UNDEFINED;
            name = UNKNOWN;
        }

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
    }
}
