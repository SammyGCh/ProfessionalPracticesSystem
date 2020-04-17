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
  		private String name;

    	public int IdIndigenousLanguage
    	{
        	get => idIndigenousLanguage;
			set => idIndigenousLanguage = value;
		}

    	public String Name
		{
        	get => name;
			set => name = value;
		}
	}
}
