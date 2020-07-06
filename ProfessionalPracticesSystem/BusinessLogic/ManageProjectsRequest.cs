/*
    Date: 12/06/2020
    Author(s): Sammy Guadarrama Chávez
 */

using System;
using System.Collections.Generic;
using DataAccess.Implementation;
using BusinessDomain;

namespace BusinessLogic
{
    public class ManageProjectsRequest
    {
        private readonly ProjectsRequestDAO projectsRequestDao;

        public ManageProjectsRequest()
        {
            projectsRequestDao = new ProjectsRequestDAO();
        }

        public bool GenerateProjectsRequest(List<Project> projectsSelected, String practitionerMatricula)
        {
            bool isGenerated = false;

            if (projectsSelected != null)
            {
                PractitionerDAO practitionerDao = new PractitionerDAO();

                Practitioner requester = practitionerDao.GetPractitionerByMatricula(practitionerMatricula);

                if (!String.IsNullOrWhiteSpace(practitionerMatricula))
                {
                    ProjectsRequest projectsRequest = new ProjectsRequest()
                    {
                        ProjectsRequested = projectsSelected,
                        RequestedBy = requester
                    };

                    isGenerated = projectsRequestDao.SaveProjectsRequest(projectsRequest);
                }                
            }

            return isGenerated;
        }

        public bool DeleteProjectsRequest(ProjectsRequest projectsRequestToDelete)
        {
            bool isDeleted = false;

            if (projectsRequestToDelete != null)
            {
                int idProjectsRequest = projectsRequestToDelete.IdProjectsRequest;
                isDeleted = projectsRequestDao.UpdateProjectsRequestStatus(idProjectsRequest);
            }

            return isDeleted;
        }
    }
}
