using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Gui
{   
    public sealed class SettingsScreen : Screen
    {
        string screenDefinitionJson = "SettingsScreenDefinition.json";
 
        ISettingsService _settingsService;
        public SettingsScreen(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }
        public override void Show()
        {
            history.Add("Settings");
            screenDefinition = ScreenDefinitionService.Load(screenDefinitionJson);
            while (true)
            {
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
                    

                    SettingsScreenChoices choice = (SettingsScreenChoices)consolChoice;
                    switch (choice)
                    {
                        case SettingsScreenChoices.ScreensSettings:
                            Console.Clear();
                            SettingsScreenShow();
                             break;

                        case SettingsScreenChoices.SaveSettings:
                            try 
                            {
                                _settingsService.Save(_settingsService.globalSettingsDictionary,"settings.json");
                            }
                            catch { WriteCustomLine(6); }
                             break;


                        case SettingsScreenChoices.Exit:
                            Console.Clear();
                            WriteCustomLine(7);
                            history.RemoveAt(history.Count-1);                           
                            return;
                    }
                }
                catch
                {
                    WriteCustomLine(8);
                }
            }

        }
        public void SettingsScreenShow() 
        {        
            while (true)
            {
                history.Add("Screen's settings");
                 //Dynamic choices creation
                List<string> classNames = CleanUpNames(GetClassNamesInNamespace());
                int count = 1;
                int consolChoice = 0;
                bool flag = false;
                while (!flag)
                {
                    HistoryShow();
                    WriteCustomLine(1);
                    if (consolChoice != 0)
                        WriteCustomLine(2);
                    else WriteHighLightedCustomLine(2);
                    count = 1;                  
                    foreach (var className in classNames)
                    {
                        if (count == consolChoice)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.WriteLine($"{count}. {className}");
                        count++;

                    }
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        Console.BackgroundColor = ConsoleColor.Black;
                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.UpArrow:
                                if (consolChoice == 0) consolChoice = count-1; else consolChoice--;
                                Console.Clear();


                                break;
                            case ConsoleKey.DownArrow:
                                if (consolChoice == count-1) consolChoice = 0; else consolChoice++;
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
                    Console.Clear ();
                    SettingsScreenChoices choice = (SettingsScreenChoices)consolChoice;
                    if (!consolChoice.Equals((int)SettingsScreenChoices.Exit))
                    {
                        history.Add($"{classNames[consolChoice-1]}'s settings");
                        string[] screenSettings = { "Background color", "Foreground color" };
                        consolChoice = 0;
                        bool flag1 = false;
                        while (!flag1)
                        {
                            HistoryShow();
                            WriteCustomLine(1);
                            if (consolChoice != 0)
                                WriteCustomLine(2);
                            else WriteHighLightedCustomLine(2);
                            count = 1;
                            foreach (var className in screenSettings)
                            {
                                if (count == consolChoice)
                                {
                                    Console.BackgroundColor = ConsoleColor.Gray;
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                }
                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                Console.WriteLine($"{count}. {className}");
                                count++;

                            }
                            ConsoleKeyInfo keyInfo = Console.ReadKey();
                            Console.BackgroundColor = ConsoleColor.Black;
                            switch (keyInfo.Key)
                            {
                                case ConsoleKey.UpArrow:
                                    if (consolChoice == 0) consolChoice = count - 1; else consolChoice--;
                                    Console.Clear();
                                    break;
                                case ConsoleKey.DownArrow:
                                    if (consolChoice == count - 1) consolChoice = 0; else consolChoice++;
                                    Console.Clear();
                                    break;
                                case ConsoleKey.Enter:
                                    flag1 = true;
                                    break;
                            }
                        }
                        Console.Clear ();
                        int? propertyChoiceAsInteger = consolChoice;
                        if (!propertyChoiceAsInteger.Equals((int)SettingsScreenChoices.Exit))
                        {
                            history.Add($"{screenSettings[consolChoice-1]}");
                            ConsoleColor[] consoleColors = AllConsoleColors();
                            consolChoice = 0;
                            bool flag2 = false;
                            while (!flag2)
                            {
                                HistoryShow();
                                WriteCustomLine(1);
                                if (consolChoice != 0)
                                    WriteCustomLine(2);
                                else WriteHighLightedCustomLine(2);
                                count = 1;
                                foreach (var className in consoleColors)
                                {
                                    if (count == consolChoice)
                                    {
                                        Console.BackgroundColor = ConsoleColor.Gray;
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                    }
                                    else
                                    {
                                        Console.BackgroundColor = ConsoleColor.Black;
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    Console.WriteLine($"{count}. {className}");
                                    count++;

                                }
                                ConsoleKeyInfo keyInfo = Console.ReadKey();
                                Console.BackgroundColor = ConsoleColor.Black;
                                switch (keyInfo.Key)
                                {
                                    case ConsoleKey.UpArrow:
                                        if (consolChoice == 0) consolChoice = count - 1; else consolChoice--;
                                        Console.Clear();


                                        break;
                                    case ConsoleKey.DownArrow:
                                        if (consolChoice == count - 1) consolChoice = 0; else consolChoice++;
                                        Console.Clear();

                                        break;
                                    case ConsoleKey.Enter:
                                        flag2 = true;
                                        history.RemoveAt(history.Count - 1);
                                        history.RemoveAt(history.Count - 1);
                                        history.RemoveAt(history.Count - 1);
                                        break;
                                }



                            }

                            Console.Clear ();
                            int? colorChoiceAsInteger = consolChoice;
                            if (!colorChoiceAsInteger.Equals((int)SettingsScreenChoices.Exit))
                            _settingsService.ApplySettings(
                                classNames[(int)(choice - 1)],
                                screenSettings[(int)(propertyChoiceAsInteger - 1)],
                                consoleColors[((int)(colorChoiceAsInteger - 1))]
                                );
                        }
                        else
                        {
                            
                            history.RemoveAt(history.Count - 1);
                            history.RemoveAt(history.Count - 1);
                        }
                    }
                    else
                    {
                      
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
        private string[] GetClassNamesInNamespace()
        {
            string?[] strings = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace == typeof(SettingsScreen).Namespace)
                .Select(t => t.AssemblyQualifiedName)
                .ToArray();

            // Filter out null elements and convert to non-nullable string[]
            string[] nonNullableStrings = strings.Where(s => s != null).Select(s => s!).ToArray();

            if (nonNullableStrings.Length == 0)
            {
                // Handle the case where there are no non-null class names
                return new string[] { "className1", "className2" };
            }
            else
            {
                return nonNullableStrings;
            }
        }
        private List<string> CleanUpNames(string[] names)
        {
            int count  = 0;
            string tName;
            List<string> tNames = new List<string>(); ;
            foreach (string name in names)
            {
                tName = name.Substring(name.IndexOf("Gui") + 4);
                tName = tName.Remove(tName.IndexOf(","));
                if (tName.EndsWith("Screen")&&!tName.Equals("Screen"))
                {
                    tNames.Add(tName);
                    count++;
                }
            }
            return tNames;
        }
        private ConsoleColor[] AllConsoleColors()//Get all console colors as array
        {
            return (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));
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
    }
}
