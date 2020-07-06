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

            isNoticeSaved = noticeHandler.SaveNotice(newNotice);

            return isNoticeSaved;
        }

        public bool UpdateNotice(Notice noticeUpdate)
        {
            bool isUpdated = false;

            isUpdated = noticeHandler.UpdateNotice(noticeUpdate);

            return isUpdated;
        }
    }
}
