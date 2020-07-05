/*
    Date: 02/07/2020
    Author(s) : Ricardo Moguel Sanchez
 */

using BusinessDomain;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface INoticeDAO
    {
        List<Notice> GetAllNotices();
        List<Notice> GetAllNoticesByAcademic(int idAcademic);
        Notice GetNoticeById(int idNotice);
        bool SaveNotice(Notice notice);
        bool UpdateNotice(Notice notice);
    }
}
