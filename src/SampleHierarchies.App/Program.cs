using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PeanutButter.TinyEventAggregator;
using SampleHierarchies.Gui;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace ImageTagger.FrontEnd.WinForms;

/// <summary>
/// Main class for starting up program.
/// </summary>
internal static class Program
{
    #region Main Method

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    /// <param name="args">Arguments</param>
    //static SettingsService settingsService;
    [STAThread]
    static void Main(string[] args)
    {

        // Create the settings service and pass the initial settings
        var initialSettings = new Dictionary<string, ISettings>();
        var settingsService = new SettingsService(initialSettings);

        // Load the settings from the JSON file
        settingsService.globalSettingsDictionary = settingsService.Read("settings.json");

        // Program start
        var host = CreateHostBuilder(settingsService).Build();
        ServiceProvider = host.Services;

        var mainScreen = ServiceProvider.GetRequiredService<MainScreen>();
        mainScreen.Show();
    }

    #endregion // Main Method

    #region Properties And Methods

    /// <summary>
    /// Service provider.
    /// </summary>
    public static IServiceProvider? ServiceProvider { get; private set; } = null;

    /// <summary>
    /// Creates a host builder.
    /// </summary>
    /// <returns></returns>
    static IHostBuilder CreateHostBuilder(SettingsService settingsService)
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Initialize initial settings
                var initialSettings = new Dictionary<string, ISettings>();
                initialSettings = settingsService.Read("settings.json");

                // Register ISettingsService with initial settings
                services.AddSingleton<ISettingsService>(_ => settingsService);
                services.AddSingleton(settingsService);
                services.AddSingleton<IEventAggregator, EventAggregator>();
                services.AddSingleton<IDataService, DataService>();
                services.AddSingleton<MainScreen, MainScreen>();
                services.AddSingleton<DogsScreen, DogsScreen>();
                services.AddSingleton<AnimalsScreen, AnimalsScreen>();
                services.AddSingleton<MammalsScreen, MammalsScreen>();
                services.AddSingleton<SettingsScreen, SettingsScreen>();
                services.AddSingleton<AfricanElephantScreen, AfricanElephantScreen>();
                services.AddSingleton<PolarBearScreen, PolarBearScreen>();
                services.AddSingleton<ChimpanzeeScreen, ChimpanzeeScreen>();
            });
    }

    #endregion // Properties And Methods
}

