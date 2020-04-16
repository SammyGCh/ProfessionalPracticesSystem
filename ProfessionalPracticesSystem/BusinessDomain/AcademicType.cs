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

    	public AcademicType()
    	{
        	idAcademicType= 0;
        	academicTypeName = "";
    	}

    	public int IdAcademicType
    	{
        	get => idAcademicType;
			set => IdAcademicType = value;
		}

    	public String AcademicTypeName
		{
			get => academicTypeName;
			set => academicTypeName = value;
		}
	}	
}