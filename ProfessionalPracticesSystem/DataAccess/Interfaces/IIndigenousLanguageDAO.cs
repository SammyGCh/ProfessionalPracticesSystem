/*
        Date: 08/04/2020                              
        Author: Cesar Sergio Martinez Palacios
 */
 
using BusinessDomain;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IIndigenousLanguageDAO
    {
        List <IndigenousLanguage> GetAllIndigenousLanguages();
        IndigenousLanguage GetLanguageById(int idIndigenousLanguage);
    }
}