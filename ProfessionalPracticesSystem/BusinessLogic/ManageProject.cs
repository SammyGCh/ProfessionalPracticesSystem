/*
    Date: 27/04/2020
    Author(s) : Sammy Guadarrama Chávez
 */

using System;
using System.Collections.Generic;
using System.Text;
using BusinessDomain;
using DataAccess.Implementation;

namespace BusinessLogic
{
    public class ManageProject
    {
        private ProjectDAO projectDao;

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
    }
}
