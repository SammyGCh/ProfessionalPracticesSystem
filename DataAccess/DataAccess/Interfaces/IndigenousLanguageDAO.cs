/*
        Date: 08/04/2020                              
        Author: Cesar Sergio Martinez Palacios
 */
 
using BusinessDomain;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IndigenousLanguageDAO
    {
        List <IndigenousLanguage> GetAllIndigenousLanguages();
        IndigenousLanguage GetIndigenousLanguage(int idIndigenousLanguage);
}
}