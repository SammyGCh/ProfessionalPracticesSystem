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
        Project GetProject(int idProject);
        Project GetProject(String name);
        List<ProjectActivity> GetAllProjectActivities(int idProject);
        bool UpdateProject(Project projectUpdated);
        bool UpdateProjectActivity(ProjectActivity projectActivityUpdated, int idProject);
        bool DeleteProject(int idProject);
    }
}
