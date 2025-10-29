using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Демонстрация синхронных vs асинхронных вызовов ===\n");

            // Синхронные вызовы
            Console.WriteLine("1. СИНХРОННЫЕ ВЫЗОВЫ:");
            var syncStopwatch = Stopwatch.StartNew();
            
            SyncMethod("Первый", 2000);
            SyncMethod("Второй", 1000);
            SyncMethod("Третий", 1500);
            
            syncStopwatch.Stop();
            Console.WriteLine($"Общее время синхронных вызовов: {syncStopwatch.ElapsedMilliseconds} мс\n");

            // Асинхронные вызовы
            Console.WriteLine("2. АСИНХРОННЫЕ ВЫЗОВЫ:");
            var asyncStopwatch = Stopwatch.StartNew();
            
            var task1 = AsyncMethod("Первый", 2000);
            var task2 = AsyncMethod("Второй", 1000);
            var task3 = AsyncMethod("Третий", 1500);
            
            // Ждем завершения всех задач
            await Task.WhenAll(task1, task2, task3);
            
            asyncStopwatch.Stop();
            Console.WriteLine($"Общее время асинхронных вызовов: {asyncStopwatch.ElapsedMilliseconds} мс\n");

            // Демонстрация с Web API вызовами
            Console.WriteLine("3. ДЕМОНСТРАЦИЯ С HTTP-ЗАПРОСАМИ:");
            await DemonstrateHttpCalls();

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void SyncMethod(string name, int delayMs)
        {
            Console.WriteLine($"  Начало синхронного метода '{name}'");
            Task.Delay(delayMs).Wait(); // Имитация работы
            Console.WriteLine($"  Конец синхронного метода '{name}' после {delayMs} мс");
        }

        static async Task AsyncMethod(string name, int delayMs)
        {
            Console.WriteLine($"  Начало асинхронного метода '{name}'");
            await Task.Delay(delayMs); // Имитация асинхронной работы
            Console.WriteLine($"  Конец асинхронного метода '{name}' после {delayMs} мс");
        }

        static async Task DemonstrateHttpCalls()
        {
            var httpClient = new System.Net.Http.HttpClient();
            
            // Имитация нескольких параллельных HTTP-запросов
            var tasks = new[]
            {
                SimulateHttpCall(httpClient, "Запрос автомобилей", 1500),
                SimulateHttpCall(httpClient, "Запрос дилеров", 1000),
                SimulateHttpCall(httpClient, "Поиск по марке", 800),
                SimulateHttpCall(httpClient, "Фильтр по цене", 1200)
            };

            Console.WriteLine("  Отправлено 4 параллельных HTTP-запроса...");
            
            var results = await Task.WhenAll(tasks);
            
            Console.WriteLine("  Все запросы завершены!");
            foreach (var result in results)
            {
                Console.WriteLine($"    - {result}");
            }
        }

        static async Task<string> SimulateHttpCall(System.Net.Http.HttpClient client, string requestName, int delayMs)
        {
            await Task.Delay(delayMs);
            return $"{requestName} завершен за {delayMs} мс";
        }
    }
}