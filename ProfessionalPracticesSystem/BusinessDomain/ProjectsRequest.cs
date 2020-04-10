/*
    Date: 09/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessDomain
{
    public class ProjectsRequest
    {
        private int idProjectsRequest;
        private String status;
        private String date;
        private List<Project> projectsRequested;
        private Practising requestedBy;

        private const int UNDEFINED = 0;
        private const String UNKNOWN = "";
        private const String DEFAULT_STATUS = "ACTIVO";

        public ProjectsRequest()
        {
            idProjectsRequest = UNDEFINED;
            status = DEFAULT_STATUS;
            date = UNKNOWN;
            projectsRequested = new List<Project>();
            requestedBy = null;
        }

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

        public Practising RequestedBy
        {
            get => requestedBy;
            set => requestedBy = value;
        }
    }
}
