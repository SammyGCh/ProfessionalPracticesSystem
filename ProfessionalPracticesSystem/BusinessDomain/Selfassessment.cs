/*
    Date: 18/06/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;
using System.Collections.Generic;

namespace BusinessDomain
{
    public class Selfassessment
    {
        private Practitioner addBy;
        private List<int> questionsValues;
        private List<String> questions;

        public Selfassessment()
        {
            questionsValues = new List<int>();
            questions = new List<String>();
            questions.Add("Mi participación en la Organización Vinculada fue productiva.");
            questions.Add("Logré la aplicación de los conocimientos teórico-prácticos adquiridos en la Licenciatura en Ingeniería de Software.");
            questions.Add("Me sentí seguro al realizar las actividades encomendadas.");
            questions.Add("Las actividades encomendadas despertaron mi interés.");
            questions.Add("La Organización Vinculada me proporcionó la información y facilidades adecuados durante el desarrollo de las prácticas.");
            questions.Add("La Organización Vinculada me dio a conocer las reglas internas que debía seguir al conducirme durante el desarrollo de las prácticas.");
            questions.Add("El Responsable del Proyecto me orientó correctamente para el desarrollo de mis actividades.");
            questions.Add("El Responsable del Proyecto realizó un seguimiento efectivo de mis actividades.");
            questions.Add("El proyecto es congruente con la formación de mi carrera.");
            questions.Add("Considero que las prácticas son importantes para mi formación profesional.");
        }

        public Practitioner AddBy
        {
            get => addBy;
            set => addBy = value;
        }

        public List<int> QuestionsValues
        {
            get => questionsValues;
            set => questionsValues = value;
        }

        public List<String> Questions
        {
            get => questions;
            set => questions = value;
        }
    }
}
