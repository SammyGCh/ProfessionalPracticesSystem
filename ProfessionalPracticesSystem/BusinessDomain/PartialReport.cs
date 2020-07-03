using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessDomain
{
    public class PartialReport
    {
        private Practitioner generatedBy;
        private String practitionerResultsAnswer;
        private String practitionerObservationsAnswer;

        public Practitioner GeneratedBy
        {
            get => generatedBy;
            set => generatedBy = value;
        }

        public String PractitionerResultsAnswer
        {
            get => practitionerResultsAnswer;
            set => practitionerResultsAnswer = value;
        }

        public String PractitionerObservationsAnswer
        {
            get => practitionerObservationsAnswer;
            set => practitionerObservationsAnswer = value;
        }
    }
}
