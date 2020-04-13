/*
    Date: 09/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Collections.Generic;

namespace BusinessDomain
{
    public class ProjectsRequest
    {
        private int idProjectsRequest;
        private String status;
        private String date;
        private List<Project> projectsRequested;
        private Practitioner requestedBy;


        public int IdProjectsRequest
        {
            get => idProjectsRequest;
            set => idProjectsRequest = value;
        }

        public String Status
        {
            get => status;
            set => status = value;
        }

        public String Date
        {
            get => date;
            set => date = value;
        }

        public List<Project> ProjectsRequested
        {
            get => projectsRequested;
            set => projectsRequested = value;
        }

        public Practitioner RequestedBy
        {
            get => requestedBy;
            set => requestedBy = value;
        }
    }
}
