/*
        Date: 08/04/2020                              
        Author:Cesar Sergio Martinez Palacios
 */

using System;

namespace BusinessDomain
{

    public class ActivityPerformed
    {
        private int idActivityPerformed;
        private ProfessorActivity generatedBy;
        private Practitioner didBy;
        private DateTime performedDate;
        private String activityReply;

        public ActivityPerformed(){}
        public int IdActivityPerformed
        {
            get => idActivityPerformed;
            set => idActivityPerformed = value;
        }
        public ProfessorActivity GeneratedBy
        {
            get => generatedBy;
            set => generatedBy = value;
        }

        public Practitioner DidBy
        {
            get => didBy;
            set => didBy = value;
        }

        public DateTime PerformedDate
        {
            get => performedDate;
            set => performedDate = value;
        }

        public String ActivityReply
        {
            get => activityReply;
            set => activityReply = value;
        }

        public static implicit operator ActivityPerformed(ActivityPerformed v)
        {
            throw new NotImplementedException();
        }
    }
}
