/*
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
        private int directUsersNumber;
        private int indirectUsersNumber;
        private int duration;
        private String generalGoal;
        private String responsabilities;
        private String mediateGoals;
        private String inmediateGoals;
        private String metology;
        private String status;
        private String neededResources;
        private int practisingNumber;
        private String generalDescription;
        private String responsableName;
        private String responsableCharge;
        private String responsableEmail;
        private String responsableTelephone;
        private DevelopmentStage belongsTo;
        private LinkedOrganization proposedBy;
        private List<ProjectActivity> projectActivities;

        private const int UNDEFINED = 0;
        private const String UNKNOWN = "";
        private const String INITIAL_STATE = "ACTIVO";

        public Project()
        {
            idProject = UNDEFINED;
            name = UNKNOWN;
            directUsersNumber = UNDEFINED;
            indirectUsersNumber = UNDEFINED;
            duration = UNDEFINED;
            generalGoal = UNKNOWN;
            responsabilities = UNKNOWN;
            mediateGoals = UNKNOWN;
            inmediateGoals = UNKNOWN;
            metology = UNKNOWN;
            status = INITIAL_STATE;
            neededResources = UNKNOWN;
            practisingNumber = UNDEFINED;
            generalDescription = UNKNOWN;
            responsableName = UNKNOWN;
            responsableCharge = UNKNOWN;
            responsableEmail = UNKNOWN;
            responsableTelephone = UNKNOWN;
            belongsTo = null;
            proposedBy = null;
        }

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

        public int DirectUsersNumber
        {
            get => directUsersNumber;
            set => directUsersNumber = value;
        }

        public int IndirectUsersNumber
        {
            get => indirectUsersNumber;
            set => indirectUsersNumber = value;
        }

        public int Duration
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

        public String Status
        {
            get => status;
            set => status = value;
        }

        public String NeededResources
        {
            get => neededResources;
            set => neededResources = value;
        }

        public int PractisingNumber
        {
            get => practisingNumber;
            set => practisingNumber = value;
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
