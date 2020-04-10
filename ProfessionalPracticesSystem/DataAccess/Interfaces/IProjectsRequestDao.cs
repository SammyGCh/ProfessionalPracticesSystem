/*
    Date: 09/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface IProjectsRequestDao
    {
        Boolean SaveProjectsRequest(ProjectsRequest projectsRequest);
        List<ProjectsRequest> GetAllProjectsRequest();
        Boolean UpdateProjectsRequest(int idProjectsRequest);
    }
}
