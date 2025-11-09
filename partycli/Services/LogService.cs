using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using partycli.Properties;

namespace partycli.Services
{
    public static class LogService
    {
        public static void Log(string action)
        {
            var newLog = new LogModel
            {
                Action = action,
                Time = DateTime.Now
            };
            List<LogModel> currentLog;
            if (!string.IsNullOrEmpty(Settings.Default.log))
            {
                currentLog = JsonConvert.DeserializeObject<List<LogModel>>(Settings.Default.log);
                currentLog.Add(newLog);
            }
            else
            {
                currentLog = new List<LogModel> { newLog };
            }

            StorageService.StoreValue("log", JsonConvert.SerializeObject(currentLog), false);
        }
    }
}