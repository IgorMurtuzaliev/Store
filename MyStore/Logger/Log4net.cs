﻿using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.Logger
{
    public static class Log4net
    {
        private static ILog log = LogManager.GetLogger("DBLog");
        public static ILog Log
        {
            get { return log; }
        }
        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }
        
    }
}