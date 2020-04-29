/*
        Date: 08/04/2020                               
        Author: Cesar Sergio Martinez Palacios
 */
 
using System;

namespace BusinessDomain
{
	public class AcademicType
	{
    	private int idAcademicType;
   		private String academicTypeName;

    	public int IdAcademicType
    	{
        	get => idAcademicType;
			set => idAcademicType = value;
		}

    	public String AcademicTypeName
		{
			get => academicTypeName;
			set => academicTypeName = value;
		}
	}	
}