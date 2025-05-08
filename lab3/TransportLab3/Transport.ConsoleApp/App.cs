using Microsoft.Extensions.Logging;
using Transport.ConsoleApp;
using Transport.Infrastructure.Models;

public class App
{
    private readonly ICrudServiceAsync<BusModel> _busService;
    private readonly ILogger<App> _logger;

    public App(ICrudServiceAsync<BusModel> busService, ILogger<App> logger)
    {
        _busService = busService;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        _logger.LogInformation("➡ Створення автобуса...");

        var newBus = new BusModel
        {
            LicensePlate = "AA 1234 BB",
            Capacity = 45,
            Driver = new DriverModel
            {
                FullName = "Петро Іванов"
            }
        };

        try
        {
            await _busService.CreateAsync(newBus);
            await _busService.SaveAsync();
            _logger.LogInformation("✅ Автобус створено.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Помилка: {ex.Message}");
        }

        _logger.LogInformation("\n📋 Усі автобуси:");
        var all = await _busService.ReadAllAsync();
        foreach (var bus in all)
        {
            _logger.LogInformation($"- {bus.LicensePlate}, {bus.Capacity} місць");
        }
    }
}
