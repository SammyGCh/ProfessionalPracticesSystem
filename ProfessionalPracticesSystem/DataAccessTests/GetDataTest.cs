using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Implementation;
using BusinessDomain;
using System.Collections.Generic;

namespace DataAccessTests
{
    [TestClass]
    public class GetDataTest
    {


        [TestMethod]
        public void GetMensualReports_PractitionerReports_ListReports()
        {
            MensualReportDAO mensualReportDao = new MensualReportDAO();
            string practitionerMatricula = "S19012186";
            List<MensualReport> mensualReports = mensualReportDao.GetAllReportsByPractitioner(practitionerMatricula);

            int expectedResult = 3;
            int obtainedResult = mensualReports.Count;
            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void GetMensualReports_ProfessorPractitionersReports_ListReports()
        {
            MensualReportDAO mensualReportDao = new MensualReportDAO();
            string professorPersonalNumber = "S99999999";
            List<MensualReport> mensualReports = mensualReportDao.GetAllReportsByAcademic(professorPersonalNumber);
            int expectedResult = 4;
            int obtainedResult = mensualReports.Count;
            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void GetIndigenousLanguages_AllIndigenousLanguages_ListIndigenousLaunguage()
        {
            IndigenousLanguageDAO indigenousLanguageDao = new IndigenousLanguageDAO();
            List<IndigenousLanguage> indigenousLanguages = indigenousLanguageDao.GetAllIndigenousLanguages();
            int expectedResult = 3;
            int obtainedResult = indigenousLanguages.Count;
            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void GetIndigenousLanguage_IndigenousLanguage_ByIdIndigenousLanguage()
        {
            int idIndigenousLanguage = 1;
            IndigenousLanguageDAO indigenousLanguageDao = new IndigenousLanguageDAO();
            IndigenousLanguage indigenousLanguage = indigenousLanguageDao.GetIndigenousLanguageById(idIndigenousLanguage);
            Assert.IsNotNull(indigenousLanguage);
        }

        [TestMethod]
        public void GetAcademicTypes_AllAcademicTypes_ListAcademicTypes()
        {
            AcademicTypeDAO academicTypeDao = new AcademicTypeDAO();
            List<AcademicType> academicTypes = academicTypeDao.GetAllAcademicTypes();
            int expectedResult = 2;
            int obtainedResult = academicTypes.Count;
            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void GetAcademicType_OneAcademicTypeById_AcademicType()
        {
            int academicTypeId = 1;
            AcademicTypeDAO academicTypeDao = new AcademicTypeDAO();
            AcademicType academicType = academicTypeDao.GetAcademicTypeById(academicTypeId);
            Assert.IsNotNull(academicType);
        }
    }
}
