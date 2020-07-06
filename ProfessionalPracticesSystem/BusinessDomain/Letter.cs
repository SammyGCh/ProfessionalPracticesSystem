/*
    Date: 01/07/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;

namespace BusinessDomain
{
    public class Letter
    {
        private Practitioner practitionerSelected;
        private Project projectSelected;
        private String coordinatorName;

        public Practitioner PractitionerSelected
        {
            get => practitionerSelected;
            set => practitionerSelected = value;
        }

        public Project ProjectSelected
        {
            get => projectSelected;
            set => projectSelected = value;
        }

        public String CoordinatorName
        {
            get => coordinatorName;
            set => coordinatorName = value;
        }
    }
}
