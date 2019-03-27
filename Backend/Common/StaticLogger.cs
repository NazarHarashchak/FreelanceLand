using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace Backend.Common
{
    public static class StaticLogger
    {
        public static void LogInfo(Type declaringType, string text)
        {
            LogManager.GetLogger(declaringType.FullName).Info(text);
        }
    }
}
