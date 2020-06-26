/*
        Date: 08/04/2020                              
        Author:Cesar Sergio Martinez Palacios
 */

using System;

namespace BusinessDomain
{
	public class IndigenousLanguage
	{
        
        private int idIndigenousLanguage;
  		private String indigenousLanguageName;
        public int status;

        public int IdIndigenousLanguage
    	{
        	get => idIndigenousLanguage;
			set => idIndigenousLanguage = value;
		}

    	public String IndigenousLanguageName
        {
        	get => indigenousLanguageName;
            set => indigenousLanguageName = value;
		}

        public int Status
        {
            get => status;
            set => status = value;
        }
    }
}
