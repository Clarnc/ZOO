using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class DogsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    string screenDefinitionJson = "DogsScreenDefinition.json";
    private IDataService _dataService;
    private SettingsService _settingsService;
    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    public DogsScreen(IDataService dataService,SettingsService settingsService)
    {
        _dataService = dataService;
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
            history.Add("Dogs");
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
                

                DogsScreenChoices choice = (DogsScreenChoices)consolChoice;
                switch (choice)
                {
                    case DogsScreenChoices.List:
                        Console.Clear();
                        ListDogs();
                        break;

                    case DogsScreenChoices.Create:
                        Console.Clear();
                        AddDog(); break;

                    case DogsScreenChoices.Delete:
                        Console.Clear();
                        DeleteDog();
                        break;

                    case DogsScreenChoices.Modify:
                        Console.Clear();
                        EditDogMain();
                        break;

                    case DogsScreenChoices.Exit:
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

    #endregion // Public Methods

    #region Private Methods

    /// <summary>
    /// List all dogs.
    /// </summary>
    private void ListDogs()
    {
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.Dogs is not null &&
            _dataService.Animals.Mammals.Dogs.Count > 0)
        {
            WriteCustomLine(11);
            int i = 1;
            foreach (Dog dog in _dataService.Animals.Mammals.Dogs)
            {
                Console.Write($"Dog number {i}, ");
                dog.Display();
                i++;
            }
        }
        else
        {
            WriteCustomLine(12);
        }
    }

    /// <summary>
    /// Add a dog.
    /// </summary>
    private void AddDog()
    {
        try
        {
            Dog dog = AddEditDog();
            _dataService?.Animals?.Mammals?.Dogs?.Add(dog);
            Console.WriteLine("Dog with name: {0} has been added to a list of dogs", dog.Name);
        }
        catch
        {
            WriteCustomLine(13);
        }
    }

    /// <summary>
    /// Deletes a dog.
    /// </summary>
    private void DeleteDog()
    {
        try
        {
            WriteCustomLine(14);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Dog? dog = (Dog?)(_dataService?.Animals?.Mammals?.Dogs
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (dog is not null)
            {
                _dataService?.Animals?.Mammals?.Dogs?.Remove(dog);
                Console.WriteLine("Dog with name: {0} has been deleted from a list of dogs", dog.Name);
            }
            else
            {
                WriteCustomLine(15);
            }
        }
        catch
        {
            WriteCustomLine(13);
        }
    }

    /// <summary>
    /// Edits an existing dog after choice made.
    /// </summary>
    private void EditDogMain()
    {
        try
        {
            WriteCustomLine(16);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Dog? dog = (Dog?)(_dataService?.Animals?.Mammals?.Dogs
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (dog is not null)
            {
                Dog dogEdited = AddEditDog();
                dog.Copy(dogEdited);
                WriteCustomLine(17);
                dog.Display();
            }
            else
            {
                WriteCustomLine(15);
            }
        }
        catch
        {
            WriteCustomLine(0);
        }
    }

    /// <summary>
    /// Adds/edit specific dog.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Dog AddEditDog()
    {
        WriteCustomLine(18);
        string? name = Console.ReadLine();
        WriteCustomLine(19);
        string? ageAsString = Console.ReadLine();
        WriteCustomLine(20);
        string? breed = Console.ReadLine();

        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (ageAsString is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (breed is null)
        {
            throw new ArgumentNullException(nameof(breed));
        }
        int age = Int32.Parse(ageAsString);
        Dog dog = new Dog(name, age, breed);

        return dog;
    }
    private void WriteAllCustomLines(int consolChoice)
    {
        WriteCustomLine(1);
        bool[] balls = new bool[5];
        balls[consolChoice] = true;
        for (int i = 0; i < balls.Length; i++)
        {
            if (!balls[i]) WriteCustomLine(i + 2);
            else WriteHighLightedCustomLine(i + 2);
        }
    }
    #endregion // Private Methods
}
