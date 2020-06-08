using System.Configuration;

namespace WebApi.App_Start
{
    /// <summary>
    /// provide static methods to retrieve configuration values
    /// </summary>
    internal class ConfigurationHelper
    {
        /// <summary>
        /// retrive connection string from application configuration
        /// </summary>
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString;
    }
}