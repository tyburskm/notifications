using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Utilities
{
    class Logs
    {
        string _cs = string.Empty;
        readonly string _fileLogPath = "c:\\temp\\.n_it_2020_l";
        public Logs(string connectionString)
        {
            _cs = connectionString;
        }

        public void LogEvent(string source, string message, System.Diagnostics.EventLogEntryType type, bool toFileOnly = false)
        {
            try
            {
                var entry = new Models.LogEntry()
                {
                    Source = source,
                    Message = message,
                    Type = type.ToString()
                };

                if (toFileOnly)
                    LogToFile(entry);
                else
                    LogToDb(entry);
            }
            catch
            {
                //surrender
            }
        }
        private void LogToDb(Models.LogEntry entry)
        {
            try
            {
                var db = new DatabaseHandler(_cs);
                db.ExecuteStoredProcedure("sp_LogEvent", null, new[] {
                    new System.Data.SqlClient.SqlParameter("@Source",entry.Source),
                    new System.Data.SqlClient.SqlParameter("@Message",entry.Message),
                    new System.Data.SqlClient.SqlParameter("@Type",entry.Type)
                });
            }
            catch
            {
                LogToFile(entry);
            }
        }

        private void LogToFile(Models.LogEntry entry)
        {
            using (var app = System.IO.File.AppendText(_fileLogPath))
            {
                app.Write($"{DateTime.Now};{entry.Source};{entry.Message};{entry.Type}\n");
            }
        }
    }
}
