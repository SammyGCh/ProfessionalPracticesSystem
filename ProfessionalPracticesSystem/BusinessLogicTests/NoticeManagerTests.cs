/*
    Date: 03/07/2020
    Author(s) : Ricardo Moguel Sanchez
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Implementation;
using BusinessDomain;
using BusinessLogic;
using System.Collections.Generic;

namespace BusinessLogicTests
{
    [TestClass]
    public class NoticeManagerTests
    {
        NoticeDAO noticeHandler = new NoticeDAO();
        ManageNotice noticeManager = new ManageNotice();
        [TestMethod]
        public void AddNotice_Success()
        {
            int noticeID = 1;
            bool testSucess = false;
            Notice notice = noticeHandler.GetNoticeById(noticeID);
            testSucess = noticeManager.AddNotice(notice);
            Assert.IsTrue(testSucess);

        }

        [TestMethod]
        public void UpdateNotice_Success()
        {
            int noticeID = 1;
            bool testSucess = false;
            Notice notice = noticeHandler.GetNoticeById(noticeID);
            testSucess = noticeManager.UpdateNotice(notice);
            Assert.IsTrue(testSucess);

        }
    }
}
