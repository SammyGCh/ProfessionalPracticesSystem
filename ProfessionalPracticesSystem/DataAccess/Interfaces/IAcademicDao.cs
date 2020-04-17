/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;
using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface IAcademicDAO
    {
        List<Academic> GetAllAcademic();
        Academic GetAcademic(int idAcademic);
        bool SaveAcademic(Academic academic);
        bool DeleteAcademic(int idAcademic);
    }
}
