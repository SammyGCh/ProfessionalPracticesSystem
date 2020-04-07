/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface IPractisingDao
    {
        List<Practising> getAllPractising();
        List<Practising> getAllPractisingByAcademic(Academic academic);
        List<Practising> getAllPractisingByindigenousLanguage(Academic academic);
        Practising getPtactising(Practising practising);
        bool deletePractising(Practising practising);
        bool savePractising();
    }
}
