/*
    Date: 26/06/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;

namespace BusinessDomain
{
    public class AssignmentLetter
    {
        private Practitioner practitionerAssigned;
        private Project projectAssigned;
        private String coordinatorName;

        public Practitioner PractitionerAssigned
        {
            get => practitionerAssigned;
            set => practitionerAssigned = value;
        }

        public Project ProjectAssigned
        {
            get => projectAssigned;
            set => projectAssigned = value;
        }

        public String CoordinatorName
        {
            get => coordinatorName;
            set => coordinatorName = value;
        }
    }
}
