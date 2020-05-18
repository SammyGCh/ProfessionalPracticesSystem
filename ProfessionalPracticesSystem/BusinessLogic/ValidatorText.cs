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
    public class ValidatorText
    {
        private static Regex userNameRegularExpression;

        public static bool IsUserName(string userName)
        {
            userNameRegularExpression = new Regex(@"\b{1}S\d{8}");

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
            Regex rightRegularExpression = new Regex(@"[a-zA-ZñÑ\s]");

            string hola = "";

            return rightRegularExpression.IsMatch(text);
        }

        public static bool IsPersonName(string name)
        {
            Regex nameRegularExpression = new Regex(@"^[\p{L}\p{M}' \.\-]+$");

            return nameRegularExpression.IsMatch(name);
        }
    }
}
