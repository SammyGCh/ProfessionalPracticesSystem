/*
    Date: 09/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface IProjectsRequestDAO
    {
        bool SaveProjectsRequest(ProjectsRequest projectsRequest);
        List<ProjectsRequest> GetAllProjectsRequestActive();
        bool UpdateProjectsRequest(int idProjectsRequest);
    }
}
