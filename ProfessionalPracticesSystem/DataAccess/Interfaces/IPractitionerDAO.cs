﻿/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System.Collections.Generic;
using BusinessDomain;
using System;

namespace DataAccess.Interfaces
{
    public interface IPractitionerDAO
    {
        List<Practitioner> GetAllPractitioner();
        List<Practitioner> GetAllPractitionerByindigenousLanguage();
        Practitioner GetPractitioner(int idPractitioner);
        Practitioner GetPractitionerByMatricula(string matricula);
        Practitioner GetPractitionerPersonalInfo(int idPractitioner);
        bool SavePractitioner(Practitioner practitioner);
        bool DeletePractitioner(int idPractitioner);
        List<Practitioner> GetAllPractitionerByAcademic(String personalNumber);
        List<Practitioner> GetAllPractitionerByProject(int idProject);
        List<Practitioner> GetAllPractitionerByLinkedOrganization(int idLinkedOrganization);
        bool UpdatePractitionerGrade(int idPractitioner);
        bool UpdatePractitioner(Practitioner updatePractitioner);
        bool UpdatePractitionerPassword(int idPractitioner, String password);
        bool AssignPractitioner(int idPractitioner, int idProject);
    }
}
