using System;
using System.Configuration;

namespace LocalSqlRepository
{
    public abstract class BaseRepository : IDisposable
    {
        internal string connString = ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString;

        /// <summary>
        /// implement IDisposable
        /// </summary>
        public void Dispose()
        {

        }
    }
}
