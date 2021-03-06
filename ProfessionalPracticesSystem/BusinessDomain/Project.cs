﻿/*
    Date: 08/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Collections.Generic;

namespace BusinessDomain
{
    public class Project
    {
        private int idProject;
        private String name;
        private String directUsersNumber;
        private String indirectUsersNumber;
        private String duration;
        private String generalGoal;
        private String responsabilities;
        private String mediateGoals;
        private String inmediateGoals;
        private String metology;
        private int status;
        private String neededResources;
        private int practitionerNumber;
        private String generalDescription;
        private String responsableName;
        private String responsableCharge;
        private String responsableEmail;
        private String responsableTelephone;
        private int practitionersAssigned;
        private DevelopmentStage belongsTo;
        private LinkedOrganization proposedBy;
        private List<ProjectActivity> projectActivities;

        public int IdProject
        {
            get => idProject;
            set => idProject = value;
        }

        public String Name
        {
            get => name;
            set => name = value;
        }

        public String DirectUsersNumber
        {
            get => directUsersNumber;
            set => directUsersNumber = value;
        }

        public String IndirectUsersNumber
        {
            get => indirectUsersNumber;
            set => indirectUsersNumber = value;
        }

        public String Duration
        {
            get => duration;
            set => duration = value;
        }

        public String GeneralGoal
        {
            get => generalGoal;
            set => generalGoal = value;
        }

        public String Responsabilities
        {
            get => responsabilities;
            set => responsabilities = value;
        }

        public String MediateGoals
        {
            get => mediateGoals;
            set => mediateGoals = value;
        }

        public String InmediateGoals
        {
            get => inmediateGoals;
            set => inmediateGoals = value;
        }

        public String Metology
        {
            get => metology;
            set => metology = value;
        }

        public int Status
        {
            get => status;
            set => status = value;
        }

        public String NeededResources
        {
            get => neededResources;
            set => neededResources = value;
        }

        public int PractitionerNumber
        {
            get => practitionerNumber;
            set => practitionerNumber = value;
        }

        public String GeneralDescription
        {
            get => generalDescription;
            set => generalDescription = value;
        }

        public String ResponsableName
        {
            get => responsableName;
            set => responsableName = value;
        }

        public String ResponsableCharge
        {
            get => responsableCharge;
            set => responsableCharge = value;
        }

        public String ResponsableEmail
        {
            get => responsableEmail;
            set => responsableEmail = value;
        }

        public String ResponsableTelephone
        {
            get => responsableTelephone;
            set => responsableTelephone = value;
        }

        public int PractitionersAssigned
        {
            get => practitionersAssigned;
            set => practitionersAssigned = value;
        }

        public DevelopmentStage BelongsTo
        {
            get => belongsTo;
            set => belongsTo = value;
        }

        public LinkedOrganization ProposedBy
        {
            get => proposedBy;
            set => proposedBy = value;
        }

        public List<ProjectActivity> ProjectActivities
        {
            get => projectActivities;
            set => projectActivities = value;
        }
    }
}
