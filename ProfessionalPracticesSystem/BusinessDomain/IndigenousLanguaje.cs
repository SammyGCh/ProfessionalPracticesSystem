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

    	public IndigenousLanguage()
    	{
        	idIndigenousLanguage= 0;
       	 	indigenousLanguageName = "";
    	}

    	public int IdIndigenousLanguage
    	{
        	get;
			set;
		}

    	public String indigenousLanguageName
		{
        	get;
			set;
		}
	}
}
