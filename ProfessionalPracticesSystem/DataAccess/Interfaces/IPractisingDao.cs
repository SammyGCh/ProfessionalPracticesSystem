/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System.Collections.Generic;
using BusinessDomain;
using System;

namespace DataAccess.Interfaces
{
    public interface IPractisingDao
    {
        List<Practising> GetAllPractising();
        List<Practising> GetAllPractisingByindigenousLanguage();
        Practising GetPractising(int idPractising);
        bool SavePractising(Practising practising);
        bool DeletePractising(int idPractising);
    }
}
