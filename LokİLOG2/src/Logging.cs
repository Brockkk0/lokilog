using Serilog;

namespace Loggin
{
    public static class Logging
    {
        public static void LoggingLog()
        {
            try
            {
                while (true)
                {
                  
                 
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true parametresi ile tuşu ekranda göstermiyoruz
                    Log.Information(keyInfo.Key.ToString());
                    // 'q' veya 'Esc' tuşlarına basıldığında döngü sonlandırılıyor
                    if (keyInfo.Key == ConsoleKey.Q || keyInfo.Key == ConsoleKey.Escape)
                    {

                        Log.Information(keyInfo.Key.ToString());
                        Console.WriteLine("\nÇıkış yapıldı.");
                        break;
                    }
                }



            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Uygulama başlatılamadı!");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

    }
}
