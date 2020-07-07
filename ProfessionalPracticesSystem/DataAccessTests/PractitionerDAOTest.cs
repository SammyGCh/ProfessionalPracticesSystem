/*
    Date: 23/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;
using System.Collections.Generic;
using BusinessDomain;
using BusinessLogic;
using DataAccess.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccessTests
{
    [TestClass]
    public class PractitionerDAOTest
    {
        readonly PractitionerDAO practitionerDAO = new PractitionerDAO();
    }
}
