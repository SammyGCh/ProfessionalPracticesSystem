/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;
using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface IAcademicDao
    {
        List<Academic> getAllAcademic();
        List<Academic> getAllAcademicByType();
        Academic getAcademic(Academic academic);
        bool saveAcademic(Academic academic);
        bool deleteAcademic(Academic academic);
    }
}
