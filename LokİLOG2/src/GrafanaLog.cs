using Serilog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog.Sinks.Grafana.Loki;
using System;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace LokiLOG.src
{
    public static class GrafanaLog
    {


        public static void GrafanaStart()
        {
            

            // Serilog yapılandırmasını ve loglama işlevini başlatma
            var configuration = GetConfiguration();

            // appsettings.json dosyasından Loki URL'sini alıyoruz
            var lokiUrl = getLokiUrl(configuration);
            // Eğer Loki URL null veya boşsa hata fırlat
            if (string.IsNullOrEmpty(lokiUrl))
            {
                throw new InvalidOperationException("Loki URL is not configured.");
            }



            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console() // Konsola log yazdırma
                .WriteTo.GrafanaLoki(lokiUrl)
                .CreateLogger();
        }
        public static IConfiguration GetConfiguration()
        {
            // Serilog yapılandırmasını ve loglama işlevini başlatma
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Null kontrolü yapın
            if (configuration == null)
            {
                throw new InvalidOperationException("Configuration could not be built.");
            }

            return configuration;
        }
        public static string getLokiUrl(IConfiguration configuration)
        {
            var lokiUrl = configuration["Loki:Url"];
            return lokiUrl;
        }
        public static void jsonChange()
        {
            string filePath = "appsettings.json";
            string pcName = Environment.MachineName;

            // JSON dosyasını oku
            var json = File.ReadAllText(filePath);
            var jsonObject = JObject.Parse(json);

            // 'Loki' etiketi altındaki 'labels' listesindeki 'value' değerini güncelle
            var labels = jsonObject["Serilog"]["WriteTo"][0]["Args"]["labels"] as JArray;

            if (labels != null)
            {
                foreach (var label in labels)
                {
                    if (label["key"].ToString() == "app")
                    {
                        label["value"] = "CLS998"; // 'app' etiketinin değerini bilgisayar adıyla değiştir
                    }
                }
            }

            // Güncellenmiş JSON'u dosyaya yaz
            File.WriteAllText(filePath, jsonObject.ToString());

            Console.WriteLine("JSON dosyası güncellendi.");


        }
    }
}
