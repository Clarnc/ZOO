using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Application main screen.
/// </summary>
public sealed class MainScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    string screenDefinitionJson = "MainScreenDefinition.json";
    private IDataService _dataService;
    private SettingsService _settingsService;
    /// <summary>
    /// Animals screen.
    /// </summary>
    private AnimalsScreen _animalsScreen;

    /// <summary>
    /// SettingsScreen
    /// </summary>
    private SettingsScreen _settingsScreen;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="animalsScreen">Animals screen</param>
    /// <param name="settingsScreen">Settings screen</param>
    public MainScreen(
        IDataService dataService,
        AnimalsScreen animalsScreen,
        SettingsScreen settingsScreen,
        SettingsService settingsService)
    {
        _dataService = dataService;
        _animalsScreen = animalsScreen;
        _settingsScreen = settingsScreen;
        _settingsService = settingsService;
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        screenDefinition = ScreenDefinitionService.Load(screenDefinitionJson);
        while (true)
        {
            
            _settingsService.ApplySettings(_settingsService.GetCallingClassName());
            int consolChoice = 0;
            bool flag = false;
            while (!flag)
            {
                
                WriteAllCustomLines(consolChoice);
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.BackgroundColor = ConsoleColor.Black;


                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (consolChoice == 0) consolChoice = 2; else consolChoice--;
                        Console.Clear();
                        break;
                    case ConsoleKey.DownArrow:
                        if (consolChoice == 2) consolChoice = 0; else consolChoice++;
                        Console.Clear();
                        break;
                    case ConsoleKey.Enter:

                        flag = true;
                        break;
                }

            }

            // Validate choice
            try
            {
                

                MainScreenChoices choice = (MainScreenChoices)consolChoice;
                switch (choice)
                {
                    case MainScreenChoices.Animals:
                        Console.Clear();
                        _animalsScreen.Show();
                        break;

                    case MainScreenChoices.Settings:
                        Console.Clear();
                        _settingsScreen.Show();
                        break;

                    case MainScreenChoices.Exit:
                        Console.Clear();
                        WriteCustomLine(6);
                        return;
                }
            }
            catch
            {
                WriteCustomLine(7);
            }
        }
    }
    private void WriteAllCustomLines(int consolChoice)
    {
        WriteCustomLine(1);
        bool[] balls = new bool[3];
        balls[consolChoice] = true;
        for (int i = 0; i < balls.Length; i++)
        {
            if (!balls[i]) WriteCustomLine(i + 2);
            else WriteHighLightedCustomLine(i + 2);
        }
    }
    #endregion // Public Methods
}