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
        Academic GetAcademicByPersonalNumber(string personalNumber);
        bool SaveAcademic(Academic academic);
        bool DeleteAcademic(int idAcademic);
        bool UpdateAcademic(Academic updatedAcademic);
        bool UpdateAcademicPassword(int idAcademic, String password);
    }
}
