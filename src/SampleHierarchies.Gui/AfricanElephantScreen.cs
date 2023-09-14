using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System.Globalization;

namespace SampleHierarchies.Gui
{
    /// <summary>
    /// Mammals main screen for African Elephants.
    /// </summary>
    public sealed class AfricanElephantScreen : Screen
    {
        #region Properties And Ctor
        private readonly string screenDefinitionJson = "AfricanElephantScreenDefinition.json";
        private readonly IDataService _dataService;
        private readonly SettingsService _settingsService;

        /// <summary>
        /// Constructor for the AfricanElephantScreen class.
        /// </summary>
        /// <param name="dataService">Data service reference</param>
        /// <param name="settingsService">Settings service reference</param>
        public AfricanElephantScreen(IDataService dataService, SettingsService settingsService)
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
                history.Add("African Elephant");
                int consolChoice = 0;
                bool flag = false;
                _settingsService.ApplySettings(_settingsService.GetCallingClassName());
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
                    

                    AfricanElephantScreenChoices choice = (AfricanElephantScreenChoices)consolChoice;
                    switch (choice)
                    {
                        case AfricanElephantScreenChoices.List:
                            Console.Clear();
                            ListAfricanElephants();
                            break;

                        case AfricanElephantScreenChoices.Create:
                            Console.Clear();
                            AddAfricanElephant();
                            break;

                        case AfricanElephantScreenChoices.Delete:
                            Console.Clear();
                            DeleteAfricanElephant();
                            break;

                        case AfricanElephantScreenChoices.Modify:
                            Console.Clear();
                            EditAfricanElephant();
                            break;

                        case AfricanElephantScreenChoices.Exit:
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

        private void ListAfricanElephants()
        {
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.AfricanElephants is not null &&
                _dataService.Animals.Mammals.AfricanElephants.Count > 0)
            {
                WriteCustomLine(11);
                int i = 1;
                foreach (AfricanElephant africanElephant in _dataService.Animals.Mammals.AfricanElephants)
                {
                    Console.Write($"African Elephant number {i}, ");
                    africanElephant.Display();
                    i++;
                }
            }
            else
            {
                WriteCustomLine(12);
            }
        }

        private void AddAfricanElephant()
        {
            try
            {
                AfricanElephant africanElephant = AddEditAfricanElephant();
                _dataService?.Animals?.Mammals?.AfricanElephants?.Add(africanElephant);
                Console.WriteLine("African Elephant with name: {0} has been added to the list of African Elephants", africanElephant.Name);
            }
            catch
            {
                WriteCustomLine(13);
            }
        }

        private void DeleteAfricanElephant()
        {
            try
            {
                WriteCustomLine(14);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                AfricanElephant? africanElephant = (AfricanElephant?)(_dataService?.Animals?.Mammals?.AfricanElephants
                    ?.FirstOrDefault(ae => ae is not null && string.Equals(ae.Name, name)));
                if (africanElephant is not null)
                {
                    _dataService?.Animals?.Mammals?.AfricanElephants?.Remove(africanElephant);
                    Console.WriteLine("African Elephant with name: {0} has been deleted from the list of African Elephants", africanElephant.Name);
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

        private void EditAfricanElephant()
        {
            try
            {
                WriteCustomLine(16);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                AfricanElephant? africanElephant = (AfricanElephant?)(_dataService?.Animals?.Mammals?.AfricanElephants
                    ?.FirstOrDefault(ae => ae is not null && string.Equals(ae.Name, name)));
                if (africanElephant is not null)
                {
                    AfricanElephant africanElephantEdited = AddEditAfricanElephant();
                    africanElephant.Copy(africanElephantEdited);
                    WriteCustomLine(17);
                    africanElephant.Display();
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

        private AfricanElephant AddEditAfricanElephant()
        {
            WriteCustomLine(18);
            string? name = Console.ReadLine();
            WriteCustomLine(19);
            string? ageAsString = Console.ReadLine();
            WriteCustomLine(20);
            string? heightAsString = Console.ReadLine();
            WriteCustomLine(21);
            string? weightAsString = Console.ReadLine();
            WriteCustomLine(22);
            string? tuskLengthAsString = Console.ReadLine();
            WriteCustomLine(23);
            string? lifespanAsString = Console.ReadLine();
            WriteCustomLine(24);
            string? socialBehavior = Console.ReadLine();

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (ageAsString is null)
            {
                throw new ArgumentNullException(nameof(ageAsString));
            }
            if (heightAsString is null)
            {
                throw new ArgumentNullException(nameof(heightAsString));
            }
            if (weightAsString is null)
            {
                throw new ArgumentNullException(nameof(weightAsString));
            }
            if (tuskLengthAsString is null)
            {
                throw new ArgumentNullException(nameof(tuskLengthAsString));
            }
            if (lifespanAsString is null)
            {
                throw new ArgumentNullException(nameof(lifespanAsString));
            }
            if (socialBehavior is null)
            {
                throw new ArgumentNullException(nameof(socialBehavior));
            }

            int age = Int32.Parse(ageAsString);
            float height = float.Parse(heightAsString, CultureInfo.InvariantCulture);
            float weight = float.Parse(weightAsString, CultureInfo.InvariantCulture);
            float tuskLength = float.Parse(tuskLengthAsString, CultureInfo.InvariantCulture);
            int lifespan = Int32.Parse(lifespanAsString);

            AfricanElephant africanElephant = new AfricanElephant(name, age, height, weight, tuskLength, lifespan, socialBehavior);

            return africanElephant;
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
}
