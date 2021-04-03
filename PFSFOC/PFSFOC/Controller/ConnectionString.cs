using System.Configuration;

namespace PFSFOC.Controller
{
    /// <summary>
    /// Класс для подключения к БД
    /// </summary>
    class ConnectionString
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        public static string ConnectoinSTR
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["PFSFOC.Properties.Settings.ConnectoinSTR"].ConnectionString;
            }
        }
    }
}
