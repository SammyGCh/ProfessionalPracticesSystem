/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface IProfessorActivityDao
    {
        List<ProfessorActivity> GetAllActivity(int idAcademic);
        bool SaveProfessorActivity(ProfessorActivity professorActivity);
        bool DeleteProfessorActivity(int idProfessorActivity);
    }
}
