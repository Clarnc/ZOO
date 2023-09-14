using System;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui
{
    /// <summary>
    /// Mammals main screen for Polar Bears.
    /// </summary>
    public sealed class PolarBearScreen : Screen
    {
        #region Properties And Ctor

        string screenDefinitionJson = "PolarBearScreenDefinition.json";
        private readonly IDataService _dataService;
        private readonly SettingsService _settingsService;

        /// <summary>
        /// Constructor for the PolarBearScreen class.
        /// </summary>
        /// <param name="dataService">Data service reference</param>
        /// <param name="settingsService">Settings service reference</param>
        public PolarBearScreen(IDataService dataService, SettingsService settingsService)
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
                history.Add("Polar Bear");
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
                    

                    PolarBearScreenChoices choice = (PolarBearScreenChoices)consolChoice;
                    switch (choice)
                    {
                        case PolarBearScreenChoices.List:
                            Console.Clear();
                            ListPolarBears();
                            break;

                        case PolarBearScreenChoices.Create:
                            Console.Clear();
                            AddPolarBear();
                            break;

                        case PolarBearScreenChoices.Delete:
                            Console.Clear();
                            DeletePolarBear();
                            break;

                        case PolarBearScreenChoices.Modify:
                            Console.Clear();
                            EditPolarBear();
                            break;

                        case PolarBearScreenChoices.Exit:
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

        private void ListPolarBears()
        {
            WriteCustomLine(11);
            if (_dataService?.Animals?.Mammals?.PolarBears is not null &&
                _dataService.Animals.Mammals.PolarBears.Count > 0)
            {
                Console.WriteLine();
                int i = 1;
                foreach (PolarBear polarBear in _dataService.Animals.Mammals.PolarBears)
                {
                    Console.Write($"Polar Bear number {i}, ");
                    polarBear.Display();
                    i++;
                }
            }
            else
            {
                WriteCustomLine(12);
            }
        }

        private void AddPolarBear()
        {
            try
            {
                PolarBear polarBear = AddEditPolarBear();
                _dataService?.Animals?.Mammals?.PolarBears?.Add(polarBear);
                Console.WriteLine("Polar Bear with name: {0} has been added to the list of Polar Bears", polarBear.Name);
            }
            catch
            {
                WriteCustomLine(13);
            }
        }

        private void DeletePolarBear()
        {
            try
            {
                WriteCustomLine(14);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                PolarBear? polarBear = (PolarBear?)(_dataService?.Animals?.Mammals?.PolarBears
                    ?.FirstOrDefault(pb => pb is not null && string.Equals(pb.Name, name)));
                if (polarBear is not null)
                {
                    _dataService?.Animals?.Mammals?.PolarBears?.Remove(polarBear);
                    Console.WriteLine("Polar Bear with name: {0} has been deleted from the list of Polar Bears", polarBear.Name);
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

        private void EditPolarBear()
        {
            try
            {
                Console.Write(16);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                PolarBear? polarBear = (PolarBear?)(_dataService?.Animals?.Mammals?.PolarBears
                    ?.FirstOrDefault(pb => pb is not null && string.Equals(pb.Name, name)));
                if (polarBear is not null)
                {
                    PolarBear polarBearEdited = AddEditPolarBear();
                    polarBear.Copy(polarBearEdited);
                    WriteCustomLine(17);
                    polarBear.Display();
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

        private PolarBear AddEditPolarBear()
        {
            WriteCustomLine(18);
            string? name = Console.ReadLine();
            WriteCustomLine(19);
            string? ageAsString = Console.ReadLine();
            WriteCustomLine(20);
            string? furCoat = Console.ReadLine();
            WriteCustomLine(21);
            string? paws = Console.ReadLine();
            WriteCustomLine(22);
            string? carnivorousDiet = Console.ReadLine();
            WriteCustomLine(23);
            string? semiAquaticAsString = Console.ReadLine();
            WriteCustomLine(24);
            string? senseOfSmell = Console.ReadLine();

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (ageAsString is null)
            {
                throw new ArgumentNullException(nameof(ageAsString));
            }
            if (furCoat is null)
            {
                throw new ArgumentNullException(nameof(furCoat));
            }
            if (paws is null)
            {
                throw new ArgumentNullException(nameof(paws));
            }
            if (carnivorousDiet is null)
            {
                throw new ArgumentNullException(nameof(carnivorousDiet));
            }
            if (semiAquaticAsString is null)
            {
                throw new ArgumentNullException(nameof(semiAquaticAsString));
            }
            if (senseOfSmell is null)
            {
                throw new ArgumentNullException(nameof(senseOfSmell));
            }

            int age = Int32.Parse(ageAsString);
            bool semiAquatic = Boolean.Parse(semiAquaticAsString);

            PolarBear polarBear = new PolarBear(name, age, furCoat, paws, carnivorousDiet, semiAquatic, senseOfSmell);

            return polarBear;
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
