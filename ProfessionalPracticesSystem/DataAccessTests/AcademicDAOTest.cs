/*
    Date: 23/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System.Collections.Generic;
using BusinessDomain;
using DataAccess.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccessTests
{
    [TestClass]
    public class AcademicDAOTest
    {
        AcademicDAO academicDAO = new AcademicDAO();

        [TestMethod]
        public void SaveAcademic_AcademicIsComplete_ReturnTrue()
        {
            AcademicType type = new AcademicType
            {
                IdAcademicType = 1
            };

            Academic newAcademic = new Academic
            {
                PersonalNumber = "1234564",
                Names = "Mario",
                Cubicle = "41",
                LastName = "Contreras",
                Gender = "Masculino",
                Password = "123456",
                BelongTo = type,
                Shift = "Matutino",
                Status = 1
            };

            bool result = academicDAO.SaveAcademic(newAcademic);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteAcademic_AcademicExist_ReturnTrue()
        {
            int idAcademic = 2;

            bool result = academicDAO.DeleteAcademic(idAcademic);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAcademic_AcademicExist_ReturnAcademic()
        {
            int idAcademic = 1;

            Academic academic = academicDAO.GetAcademic(idAcademic);
        }

        [TestMethod]
        public void GetAllAcademic_WhatAcademicExist_ReturnAcademicList()
        {
            List<Academic> result = academicDAO.GetAllAcademic();

            Assert.IsTrue(result.Count > 0);
        }
    }
}
