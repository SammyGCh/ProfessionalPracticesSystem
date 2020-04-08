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
        private const int UNDEFINED = 0;
        private const String UNKNOWN = "";

        public LinkedOrganization()
        {
            idLinkedOrganization = UNDEFINED;
            city = UNKNOWN;
            email = UNKNOWN;
            state = "Veracruz";
            name = UNKNOWN;
            telephoneNumber = UNKNOWN;
            address = UNKNOWN;
            belongsTo = null;
        }

        public int IdLinkedOrganization
        {
            get;
            set;
        }

        public String City
        {
            get;
            set;
        }

        public String Email
        {
            get;
            set;
        }

        public String State
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public String TelephoneNumber
        {
            get;
            set;
        }

        public String Address
        {
            get;
            set;
        }

        public OrganizationSector BelongsTo
        {
            get;
            set;
        }
    }
}
