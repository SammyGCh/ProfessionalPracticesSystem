/*
    Date: 08/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface IProjectDAO
    {
        bool SaveProject(Project project);
        bool SaveProjectActivity(ProjectActivity projectActivity, String projectName);
        List<Project> GetAllProjects();
        List<Project> GetActiveProjects();
        Project GetProjectById(int idProject);
        Project GetProjectByName(String name);
        int GetIdProjectByName(String name);
        Project GetProjectRequestInfo(int idProject);
        Project GetProjectAvailabilityInfo(int idProject);
        List<ProjectActivity> GetAllProjectActivities(int idProject);
        bool UpdateProject(Project projectUpdated);
        bool UpdateProjectData(Project projectDataUpdated);
        bool UpdateProjectResponsableData(Project projectResponsableData);
        bool UpdateLinkedOrganizationOfProject(int idProject, int idLinkedOrganization);
        bool UpdateProjectActivity(ProjectActivity projectActivityUpdated, int idProject);
        bool UpdateAvailabilityProjectStatus(int idProject);
        bool DeleteProject(int idProject);
        bool DeleteProjectActivity(int idProjectActivity);
    }
}
