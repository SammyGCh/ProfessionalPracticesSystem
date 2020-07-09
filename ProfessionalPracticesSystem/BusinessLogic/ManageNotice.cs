/*
        Date: 02/07/2020                              
        Author:Ricardo Moguel Sanchez
 */

using System;
using BusinessDomain;
using DataAccess.Implementation;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class ManageNotice
    {
        private NoticeDAO noticeHandler;

        public ManageNotice()
        {
            noticeHandler = new NoticeDAO();
        }

        public bool AddNotice(Notice newNotice)
        {
            bool isNoticeSaved = false;

            newNotice.CreationDate = GetPublicationTime(newNotice);

            isNoticeSaved = noticeHandler.SaveNotice(newNotice);

            return isNoticeSaved;
        }

        public bool UpdateNotice(Notice noticeUpdate)
        {
            bool isUpdated = false;

            noticeUpdate.CreationDate = GetPublicationTime(noticeUpdate);

            isUpdated = noticeHandler.UpdateNotice(noticeUpdate);

            return isUpdated;
        }

        public String GetPublicationTime(Notice notice)
        {
            String creationTime = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");

            return creationTime;
        }
    }
}
