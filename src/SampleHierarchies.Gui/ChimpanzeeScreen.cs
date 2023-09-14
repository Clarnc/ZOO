using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System;
using System.Linq;

namespace SampleHierarchies.Gui
{
    /// <summary>
    /// Mammals main screen for Chimpanzees.
    /// </summary>
    public sealed class ChimpanzeeScreen : Screen
    {
        #region Properties And Ctor

        string screenDefinitionJson = "ChimpanzeeScreenDefinition.json";
        private readonly IDataService _dataService;
        private readonly SettingsService _settingsService;

        /// <summary>
        /// Constructor for the ChimpanzeeScreen class.
        /// </summary>
        /// <param name="dataService">Data service reference</param>
        /// <param name="settingsService">Settings service reference</param>
        public ChimpanzeeScreen(IDataService dataService, SettingsService settingsService)
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
                history.Add("Chimpanzees");
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
                   

                    ChimpanzeeScreenChoices choice = (ChimpanzeeScreenChoices)consolChoice;
                    switch (choice)
                    {
                        case ChimpanzeeScreenChoices.List:
                            Console.Clear();
                            ListChimpanzees();
                            break;

                        case ChimpanzeeScreenChoices.Create:
                            Console.Clear();
                            AddChimpanzee();
                            break;

                        case ChimpanzeeScreenChoices.Delete:
                            Console.Clear();
                            DeleteChimpanzee();
                            break;

                        case ChimpanzeeScreenChoices.Modify:
                            Console.Clear();
                            EditChimpanzee();
                            break;

                        case ChimpanzeeScreenChoices.Exit:
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

        private void ListChimpanzees()
        {
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.Chimpanzees is not null &&
                _dataService.Animals.Mammals.Chimpanzees.Count > 0)
            {
                WriteCustomLine(11);
                int i = 1;
                foreach (Chimpanzee chimpanzee in _dataService.Animals.Mammals.Chimpanzees)
                {
                    Console.Write($"Chimpanzee number {i}, ");
                    chimpanzee.Display();
                    i++;
                }
            }
            else
            {
                WriteCustomLine(12);
            }
        }

        private void AddChimpanzee()
        {
            try
            {
                Chimpanzee chimpanzee = AddEditChimpanzee();
                _dataService?.Animals?.Mammals?.Chimpanzees?.Add(chimpanzee);
                Console.WriteLine("Chimpanzee with name: {0} has been added to the list of Chimpanzees", chimpanzee.Name);
            }
            catch
            {
                WriteCustomLine(13);
            }
        }

        private void DeleteChimpanzee()
        {
            try
            {
                WriteCustomLine(14);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Chimpanzee? chimpanzee = (Chimpanzee?)(_dataService?.Animals?.Mammals?.Chimpanzees
                    ?.FirstOrDefault(c => c is not null && string.Equals(c.Name, name)));
                if (chimpanzee is not null)
                {
                    _dataService?.Animals?.Mammals?.Chimpanzees?.Remove(chimpanzee);
                    Console.WriteLine("Chimpanzee with name: {0} has been deleted from the list of Chimpanzees", chimpanzee.Name);
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

        private void EditChimpanzee()
        {
            try
            {
                WriteCustomLine(16);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Chimpanzee? chimpanzee = (Chimpanzee?)(_dataService?.Animals?.Mammals?.Chimpanzees
                    ?.FirstOrDefault(c => c is not null && string.Equals(c.Name, name)));
                if (chimpanzee is not null)
                {
                    Chimpanzee chimpanzeeEdited = AddEditChimpanzee();
                    chimpanzee.Copy(chimpanzeeEdited);
                    WriteCustomLine(17);
                    chimpanzee.Display();
                }
                else
                {
                    WriteCustomLine(15);
                }
            }
            catch
            {
                WriteCustomLine(10);
            }
        }

        private Chimpanzee AddEditChimpanzee()
        {
            WriteCustomLine(18);
            string? name = Console.ReadLine();
            WriteCustomLine(19);
            string? ageAsString = Console.ReadLine();
            WriteCustomLine(20);
            string? input = Console.ReadLine();
            bool opposableThumbs = false;
            if (!string.IsNullOrEmpty(input))
            {
                opposableThumbs = bool.Parse(input);              
            }
            WriteCustomLine(21);
            string? socialBehavior = Console.ReadLine();           
            WriteCustomLine(22);
            input = Console.ReadLine();
            bool toolUse = false;
            if (!string.IsNullOrEmpty(input))
            {
                toolUse = bool.Parse(input);
            }
            WriteCustomLine(23);
            input = Console.ReadLine();
            int intelligence = 0;
            if (!string.IsNullOrEmpty(input))
            {
                intelligence = Int32.Parse(input);
            }
            WriteCustomLine(24);
            string? diet = Console.ReadLine();

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (ageAsString is null)
            {
                throw new ArgumentNullException(nameof(ageAsString));
            }
            if (socialBehavior is null)
            {
                throw new ArgumentNullException(nameof(socialBehavior));
            }
            if (diet is null)
            {
                throw new ArgumentNullException(nameof(diet));
            }

            int age = int.Parse(ageAsString);

            Chimpanzee chimpanzee = new Chimpanzee(name, age, opposableThumbs, socialBehavior, toolUse, intelligence, diet);

            return chimpanzee;
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
