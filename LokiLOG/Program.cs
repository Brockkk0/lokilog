using LokiLOG.src;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Grafana.Loki;
using System;
using LokiLOG;
using Loggin;

namespace LokiGraf.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GrafanaLog.jsonChange();
            Thread.Sleep(1000);
            GrafanaLog.GrafanaStart();
            Logging.LoggingLog();

        }
    }
}
