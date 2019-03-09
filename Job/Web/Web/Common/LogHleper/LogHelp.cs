using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace Web.Common.LogHleper
{
    public class LogHelp
    {
      static  NLog.Logger logger = LogManager.GetCurrentClassLogger();

        public static void Debug(string info)
        {
            logger.Debug(info);
        }
        public static void Debug(Exception ex ,string info)
        {
            logger.Debug(ex,info="");
        }
        public static void Info(string info)
        {
            logger.Info(info);
        }
        public static void Error(string info)
        {
            logger.Error(info);
        }
        public static void Error(Exception ex, string info=null)
        {
            logger.Error(ex, info);
        }
    }
}
