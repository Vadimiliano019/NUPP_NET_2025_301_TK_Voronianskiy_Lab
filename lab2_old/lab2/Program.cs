using lab2;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace lab2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var service = new InMemoryCrudServiceAsync<Bus>("buses.json");

            var tasks = Enumerable.Range(0, 1000)
                .Select(async _ =>
                {
                    var bus = new Bus(); // ← тут виправлено
                    bus = Bus.CreateNew(); // ← якщо це статичний метод, все ок
                    await service.CreateAsync(bus);
                });

            await Task.WhenAll(tasks);

            var allBuses = await service.ReadAllAsync();
            var maxMileage = allBuses.Max(b => b.Mileage);
            var minMileage = allBuses.Min(b => b.Mileage);
            var avgMileage = allBuses.Average(b => b.Mileage);

            Console.WriteLine($"MAX mileage: {maxMileage:F2}");
            Console.WriteLine($"MIN mileage: {minMileage:F2}");
            Console.WriteLine($"AVG mileage: {avgMileage:F2}");

            var result = await service.SaveAsync();
            Console.WriteLine(result ? "Дані збережено!" : "Помилка збереження.");
        }
    }
}
