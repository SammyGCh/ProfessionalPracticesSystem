/*
    Date: 12/06/2020
    Author(s): Sammy Guadarrama Chávez
 */

using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Implementation;
using BusinessDomain;

namespace BusinessLogic
{
    public class ManageProjectsRequest
    {
        private ProjectsRequestDAO projectsRequestDao;

        public ManageProjectsRequest()
        {
            projectsRequestDao = new ProjectsRequestDAO();
        }

        public bool GenerateProjectsRequest(List<Project> projectsSelected, Practitioner requester)
        {
            bool isGenerated = false;

            if (projectsSelected != null && requester != null)
            {
                ProjectsRequest projectsRequest = new ProjectsRequest() { 
                    ProjectsRequested = projectsSelected,
                    RequestedBy = requester
                };

                isGenerated = projectsRequestDao.SaveProjectsRequest(projectsRequest);
            }

            return isGenerated;
        }
    }
}
