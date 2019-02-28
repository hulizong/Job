using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
   public  class LogHelper
    {
        static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Debug(string info)
        {
            logger.Debug(info);
        }
        public static void Info(string info)
        {
            logger.Info(info);
        }
        public static void Error(Exception ex, string info = "")
        {
            logger.Error(ex, info);
        }
        public static void Error(string ex, string info = "")
        {
            logger.Error(ex, info);
        }
    }
}
