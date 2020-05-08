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
    }
}
