using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Animals main screen.
/// </summary>
public sealed class AnimalsScreen : Screen
{
    #region Properties And Ctor


    string screenDefinitionJson = "AnimalsScreenDefinition.json";
    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;

    /// <summary>
    /// Animals screen.
    /// </summary>
    private MammalsScreen _mammalsScreen;

    private SettingsService _settingsService;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="animalsScreen">Animals screen</param>
    public AnimalsScreen(
        IDataService dataService,
        MammalsScreen mammalsScreen,
        SettingsService settingsService
        )
    {
        _dataService = dataService;
        _mammalsScreen = mammalsScreen;
        _settingsService = settingsService;
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        while (true)
        {
            history.Add("Animals");
            screenDefinition = ScreenDefinitionService.Load(screenDefinitionJson);
            _settingsService.ApplySettings(_settingsService.GetCallingClassName());
            int consolChoice = 0;
            bool flag = false;
            while (!flag)
            {
                HistoryShow();
                WriteAllCustomLines(consolChoice);
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.BackgroundColor = ConsoleColor.Black;


                switch (keyInfo.Key)
                {

                    case ConsoleKey.UpArrow:
                        if (consolChoice == 0) consolChoice = 3; else consolChoice--;
                        Console.Clear();
                        
                        break;
                    case ConsoleKey.DownArrow:
                        if (consolChoice == 3) consolChoice = 0; else consolChoice++;
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
               

                AnimalsScreenChoices choice = (AnimalsScreenChoices)consolChoice;
                switch (choice)
                {
                    case AnimalsScreenChoices.Mammals:
                        Console.Clear();
                        _mammalsScreen.Show();
                        break;

                    case AnimalsScreenChoices.Read:
                        Console.Clear();
                        ReadFromFile();
                        break;

                    case AnimalsScreenChoices.Save:
                        Console.Clear();
                        SaveToFile();
                        break;

                    case AnimalsScreenChoices.Exit:
                        Console.Clear();
                        history.RemoveAt(history.Count - 1);
                        return;
                }
            }
            catch
            {
                WriteCustomLine(8);
            }
        }
    }

    #endregion // Public Methods

    #region Private Methods

    /// <summary>
    /// Save to file.
    /// </summary>
    private void SaveToFile()
    {
        try
        {
            WriteCustomLine(9);
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Write(fileName);
            Console.WriteLine("Data saving to: '{0}' was successful.", fileName);
        }
        catch
        {
            WriteCustomLine(10);
        }
    }

    /// <summary>
    /// Read data from file.
    /// </summary>
    private void ReadFromFile()
    {
        try
        {
            WriteCustomLine(11);
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Read(fileName);
            Console.WriteLine("Data reading from: '{0}' was successful.", fileName);
        }
        catch
        {
            WriteCustomLine(12);
        }
    }
    private void WriteAllCustomLines(int consolChoice)
    {
        WriteCustomLine(1);
        bool[] balls = new bool[4];
        balls[consolChoice] = true;
        for (int i = 0; i < 4; i++)
        {
            if (!balls[i]) WriteCustomLine(i + 2);
            else WriteHighLightedCustomLine(i + 2);
        }
    }
    #endregion // Private Methods
}
