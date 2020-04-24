/*
    Date: 06/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;

namespace DataAccess
{
    public class LogManager
    {
        private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void WriteLog(string message, Exception ex)
        {
            log.Error(message, ex);
        }
    }
}
