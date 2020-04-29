/*
        Date: 08/04/2020                              
        Author:Cesar Sergio Martinez Palacios
 */

using System;

namespace BusinessDomain
{

    public class ActivityPerformed
    {
        private ProfessorActivity generatedBy;
        private Practitioner performedBy;
        private String performedDate;
        private String activityReply;
        private String observations;

        public ActivityPerformed(){}
        public ProfessorActivity GeneratedBy
        {
            get => generatedBy;
            set => generatedBy = value;
        }

        public Practitioner PerformedBy
        {
            get => performedBy;
            set => performedBy = value;
        }

        public String PerformedDate
        {
            get => performedDate;
            set => performedDate = value;
        }

        public String ActivityReply
        {
            get => activityReply;
            set => activityReply = value;
        }

        public String Observations
        {
            get => observations;
            set => observations = value;
        }
    }
}
