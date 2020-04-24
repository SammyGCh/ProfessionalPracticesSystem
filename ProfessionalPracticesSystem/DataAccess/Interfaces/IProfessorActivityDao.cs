/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface IProfessorActivityDAO
    {
        List<ProfessorActivity> GetAllProfessorActivityByAcademic(int idAcademic);
        bool SaveProfessorActivity(ProfessorActivity professorActivity);
        ProfessorActivity GetProfessorActivity(int idProfessorActivity);
        bool DeleteProfessorActivity(int idProfessorActivity);
    }
}
