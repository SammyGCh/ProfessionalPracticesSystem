/*
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
        bool SavePractitioner(Practitioner practitioner);
        bool DeletePractitioner(int idPractitioner);
        List<Practitioner> GetAllPractitionerByAcademic(int idAcademic);
        List<Practitioner> GetAllPractitionerByProject(int idProject);
        List<Practitioner> GetAllPractitionerByLinkedOrganization(int idLinkedOrganization);
        bool UpdatePractitionerGrade(int idPractitioner);
    }
}
