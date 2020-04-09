/*
    Date: 08/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface IProjectDao
    {
        Boolean SaveProject(Project project);
        Boolean SaveProjectActivity(ProjectActivity projectActivity, String projectName);
        List<Project> GetAllProjects();
        Project GetProject(int idProject);
        Project GetProject(String name);
        List<ProjectActivity> GetAllProjectActivities(int idProject);
        Boolean UpdateProject(Project projectUpdated);
        Boolean UpdateProjectActivity(ProjectActivity projectActivityUpdated, int idProject);
        Boolean DeleteProject(int idProject);
    }
}
