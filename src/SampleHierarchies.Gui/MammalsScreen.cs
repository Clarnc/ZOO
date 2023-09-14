using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class MammalsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Animals screen.
    /// </summary>
    string screenDefinitionJson = "MammalsScreenDefinition.json";
    private DogsScreen _dogsScreen;
    private AfricanElephantScreen _africanElephantScreen;
    private PolarBearScreen _polarBearScreen;
    private ChimpanzeeScreen _chimpanzeeScreen;
    private SettingsService _settingsService;
    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="dogsScreen">Dogs screen</param>
    public MammalsScreen(DogsScreen dogsScreen, SettingsService settingsService,AfricanElephantScreen africanElephantScreen, PolarBearScreen polarBearScreen, ChimpanzeeScreen chimpanzeeScreen)
    {
        _dogsScreen = dogsScreen;
        _settingsService = settingsService;
        _africanElephantScreen = africanElephantScreen;
        _polarBearScreen = polarBearScreen;
        _chimpanzeeScreen = chimpanzeeScreen;
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        screenDefinition = ScreenDefinitionService.Load(screenDefinitionJson);
        _settingsService.ApplySettings(_settingsService.GetCallingClassName());
        while (true)
        {
            history.Add("Mammale");
            int consolChoice = 0;
            bool flag = false;
            while (!flag) {
                HistoryShow();
                WriteAllCustomLines(consolChoice);
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.BackgroundColor = ConsoleColor.Black;
                
               
                switch (keyInfo.Key) 
                {
                    case ConsoleKey.UpArrow:
                        if (consolChoice == 0) consolChoice = 4; else consolChoice--;
                        Console.Clear();
                        
                        break; 
                    case ConsoleKey.DownArrow:
                        if (consolChoice == 4) consolChoice = 0; else consolChoice++;
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

                MammalsScreenChoices choice = (MammalsScreenChoices)consolChoice;
                switch (choice)
                {
                    case MammalsScreenChoices.Dogs:
                        Console.Clear();
                        _dogsScreen.Show();
                        break;
                    case MammalsScreenChoices.AfricanElephants:
                        Console.Clear();
                        _africanElephantScreen.Show();
                        break;
                    case MammalsScreenChoices.PolarBears:
                        Console.Clear();
                        _polarBearScreen.Show();
                        break;
                    case MammalsScreenChoices.Chimpanzees:
                        Console.Clear();
                        _chimpanzeeScreen.Show();
                        break;

                    case MammalsScreenChoices.Exit:
                        Console.Clear();
                        history.RemoveAt(history.Count - 1);
                        return;
                }
            }
            catch
            {
                WriteCustomLine(10);
            }
        }
    }

    private void WriteAllCustomLines(int consolChoice)
    {
        WriteCustomLine(1);
        bool[] balls = new bool[5];
        balls[consolChoice] = true;
        for (int i = 0; i < 5; i++) {
            if (!balls[i]) WriteCustomLine(i + 2);
            else WriteHighLightedCustomLine(i + 2);
        }
    }

    #endregion // Public Methods
}
