/*
        Date: 05/07/2020                              
        Author:Cesar Sergio Martinez Palacios
 */

using System.Collections.Generic;
using BusinessDomain;
using DataAccess.Implementation;

namespace BusinessLogic
{

    public static class StatisticsListsManage
    {
        public static List<ScholarPeriod> GetScholarPeriods()
        {
            ScholarPeriodDAO scholarPeriodDAO = new ScholarPeriodDAO();
            List<ScholarPeriod> allPeriods = scholarPeriodDAO.GetAllScholarPeriods();
            return allPeriods;
        }

        public static List<DevelopmentStage> GetDevelopmentStages()
        {
            DevelopmentStageDAO developmentStageDAO = new DevelopmentStageDAO();
            List<DevelopmentStage> allStages = developmentStageDAO.GetAllDevelopmentStages();
            return allStages;
        }

        public static List<LinkedOrganization> GetLinkedOrganizations()
        {
            LinkedOrganizationDAO linkedOrganizationDAO = new LinkedOrganizationDAO();
            List<LinkedOrganization> allOrganizations = linkedOrganizationDAO.GetAllLinkedOrganizations();
            return allOrganizations;
        }

        public static List<OrganizationSector> GetOrganizationSectors()
        {
            OrganizationSectorDAO sectorDAO = new OrganizationSectorDAO();
            List<OrganizationSector> allSectors = sectorDAO.GetAllOrganizationSectors();
            return allSectors;
        }

        public static List<Practitioner> GetPractitioners()
        {
            PractitionerDAO practitionerDAO = new PractitionerDAO();
            List<Practitioner> allPractitioners = practitionerDAO.GetAllPractitioner();
            return allPractitioners;
        }
        public static List<Project> GetProjects()
        {
            ProjectDAO projectDAO = new ProjectDAO();
            List<Project> allProjects = projectDAO.GetAllProjects();
            return allProjects;
        }
    }
}
