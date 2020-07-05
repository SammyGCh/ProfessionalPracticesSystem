/*
    Date: 02/05/2020
    Author(s): Sammy Guadarrama Chávez
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    public static class ValidatorText
    {
        private const int MINIMUM_LENGHT = 10;
        private const int MINIMUM_LENGHT_MENSUAL_REPORT = 200;
        private const int MINIMUM_LENGHT_PARTIAL_REPORT = 200;
        private const int MINIMUM_LENGHT_OBSERVATIONS_REPORT = 100;

        public static bool IsUserName(string userName)
        {
            Regex userNameRegularExpression = new Regex(@"\b{1}S\d{8}");

            return userNameRegularExpression.IsMatch(userName);
        }

        public static bool IsTelephoneNumber(string telephoneNumber)
        {
            Regex telephoneNumberRegularExpression = new Regex(@"\d");

            return telephoneNumberRegularExpression.IsMatch(telephoneNumber);
        }

        public static bool IsEmail(string email)
        {
            Regex emailRegularExpresion = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            return emailRegularExpresion.IsMatch(email);
        }

        public static bool IsRightExpression(string text)
        {
            Regex rightRegularExpression = new Regex(@"([a-zA-Z]{1,}\s{0,1}){10,}");

            return rightRegularExpression.IsMatch(text);
        }

        public static bool IsPersonName(string name)
        {
            Regex nameRegularExpression = new Regex(@"^[\p{L}\p{M}' \.\-]+$");

            return nameRegularExpression.IsMatch(name);
        }

        public static bool IsANumber(string number)
        {
            Regex numberRegularExpression = new Regex(@"(\d{1,4})$");

            return numberRegularExpression.IsMatch(number);
        }

        public static bool IsAValidGrade(string number)
        {
            bool isValid = false;
            Regex numberRegularExpression = new Regex(@"(\d{1,4})$");

            if (numberRegularExpression.IsMatch(number))
            {
                float grade = float.Parse(number);

                isValid = (grade > 0.00) && (grade <= 10.00);
            }

            return isValid;
        }

        public static bool IsTextRight(string textToValidate)
        {
            bool isTextRight = false;

            if (IsRightExpression(textToValidate) && textToValidate.Length > MINIMUM_LENGHT)
            {
                isTextRight = true;
            }

            return isTextRight;
        }

        public static bool IsMensualReportTextRight(string textToValidate)
        {
            bool isTextRight = false;

            if (IsRightExpression(textToValidate) && textToValidate.Length > MINIMUM_LENGHT_MENSUAL_REPORT)
            {
                isTextRight = true;
            }

            return isTextRight;
        }

        public static bool IsPartialReportTextRight(string textToValidate)
        {
            bool isTextRight = false;

            if (IsRightExpression(textToValidate) && textToValidate.Length > MINIMUM_LENGHT_PARTIAL_REPORT)
            {
                isTextRight = true;
            }

            return isTextRight;
        }

        public static bool IsPartialReportobservationsRight(string textToValidate)
        {
            bool isTextRight = false;

            if (IsRightExpression(textToValidate) && textToValidate.Length > MINIMUM_LENGHT_OBSERVATIONS_REPORT)
            {
                isTextRight = true;
            }

            return isTextRight;
        }
    }
}