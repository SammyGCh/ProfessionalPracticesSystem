﻿/*
    Date: 27/04/2020
    Author(s) : Sammy Guadarrama Chávez
 */

using BusinessDomain;
using DataAccess.Implementation;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class ManageProject
    {
        private readonly ProjectDAO projectDao;
        private const int INVALID_ID = 0;

        public ManageProject()
        {
            projectDao = new ProjectDAO();
        }

        public bool SaveProject(Project project)
        {
            bool isProjectSaved;
            bool isAllSaved = false;

            isProjectSaved = projectDao.SaveProject(project);

            if (isProjectSaved)
            {
                foreach (ProjectActivity activity in project.ProjectActivities)
                {
                    isAllSaved = projectDao.SaveProjectActivity(activity, project.Name);
                }
            }

            return isAllSaved;
        }

        public bool UpdateProjectData(Project projectUpdated)
        {
            bool isUpdated;

            isUpdated = projectDao.UpdateProjectData(projectUpdated);

            return isUpdated;
        }

        public bool UpdateProjectResponsableData(Project projectResponsableData)
        {
            bool isUpdated;

            isUpdated = projectDao.UpdateProjectResponsableData(projectResponsableData);

            return isUpdated;
        }

        public bool UpdateLinkedOrganizationOfProject(int idProject, int idLinkedOrganization)
        {
            bool isUpdated;

            isUpdated = projectDao.UpdateLinkedOrganizationOfProject(idProject, idLinkedOrganization);

            return isUpdated;
        }

        public bool DeleteProjectActivity(ProjectActivity projectActivity)
        {
            bool isDeleted;
            int idProjectActivity = projectActivity.IdProjectActivity;

            isDeleted = projectDao.DeleteProjectActivity(idProjectActivity);

            return isDeleted;
        }

        public bool AddProjectActivities(List<ProjectActivity> newProjectActivities, string projectName)
        {
            bool areAdded = false;

            foreach (ProjectActivity newProjectActivity in newProjectActivities)
            {
                areAdded = projectDao.SaveProjectActivity(newProjectActivity, projectName);
            }

            return areAdded;
        }

        public bool UpdateProjectStatus(int idProjectToUpdate)
        {
            bool isStatusUpdated = false;

            if(idProjectToUpdate != INVALID_ID)
            {
                isStatusUpdated = projectDao.DeleteProject(idProjectToUpdate);
            }

            return isStatusUpdated;
        }
    }
}
