/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface IProfessorActivity
    {
        //List<ProfessorActivity> getAllActivityByProfessor(Professor profesor);
        List<ProfessorActivity> getAllActivityByPractising(Practising practising);
        bool updateProfessorActivity(ProfessorActivity profesorActiivity);
        bool saveProfessorActivity(ProfessorActivity professorActivity);
        bool deleteProfessorActivity(ProfessorActivity profesorActiivity);
    }
}
