/*
    Date: 07/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;

namespace BusinessDomain
{
    public class LinkedOrganization
    {
        private int idLinkedOrganization;
        private String city;
        private String email;
        private String state;
        private String name;
        private String telephoneNumber;
        private String address;
        private OrganizationSector belongsTo;

        public int IdLinkedOrganization
        {
            get => idLinkedOrganization;
            set => idLinkedOrganization = value;
        }

        public String City
        {
            get => city;
            set => city = value;
        }

        public String Email
        {
            get => email;
            set => email = value;
        }

        public String State
        {
            get => state;
            set => state = value;
        }

        public String Name
        {
            get => name;
            set => name = value;
        }

        public String TelephoneNumber
        {
            get => telephoneNumber;
            set => telephoneNumber = value;
        }

        public String Address
        {
            get => address;
            set => address = value;
        }

        public OrganizationSector BelongsTo
        {
            get => belongsTo;
            set => belongsTo = value;
        }
    }
}
