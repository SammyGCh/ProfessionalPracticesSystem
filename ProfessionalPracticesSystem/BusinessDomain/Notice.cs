/*
    Date: 02/07/2020
    Author(s) : Ricardo Moguel Sanchez
 */

using System;

namespace BusinessDomain
{
    public class Notice
    {
        private int idNotice;
        private String title;
        private String body;
        private String creationDate;
        private Academic createdBy;

        public Notice() {}

        public int IdNotice
        {
            get => idNotice;
            set => idNotice = value;
        }

        public String Title
        {
            get => title;
            set => title = value;
        }

        public String Body
        {
            get => body;
            set => body = value;
        }

        public String CreationDate
        {
            get => creationDate;
            set => creationDate = value;
        }

        public Academic CreatedBy
        {
            get => createdBy;
            set => createdBy = value;
        }
    }
}
