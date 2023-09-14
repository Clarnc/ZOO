using Newtonsoft.Json;
using SampleHierarchies.Data;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace SampleHierarchies.Services
{
    /// <summary>
    /// Settings service.
    /// </summary>
    public class SettingsService : ISettingsService
    {
        // Global settings Dictionary
        public Dictionary<string, ISettings> globalSettingsDictionary { get; set; }

        public SettingsService(Dictionary<string, ISettings>? initialSettings)
        {
            globalSettingsDictionary = initialSettings ?? new Dictionary<string, ISettings>();
        }

        // Applying settings from globalSettingsDictionary
        public void ApplySettings(string className)
        {
            if (globalSettingsDictionary.ContainsKey(className))
            {
                var settings = globalSettingsDictionary[className];
                if (settings != null) // Check for null
                {
                    if (Enum.TryParse(settings.BackgroundColor, out ConsoleColor backgroundColor) &&
                        Enum.TryParse(settings.ForegroundColor, out ConsoleColor foregroundColor))
                    {
                        Console.BackgroundColor = backgroundColor;
                        Console.ForegroundColor = foregroundColor;
                    }
                }
            }
        }

        // Applying settings from settings screen
        public void ApplySettings(string className, string property, ConsoleColor color)
        {
            switch (property)
            {
                case "Foreground color":
                    {
                        Console.ForegroundColor = color;
                        if (globalSettingsDictionary.ContainsKey(className))
                        {
                            globalSettingsDictionary[className].ForegroundColor = color.ToString();
                        }
                    }
                    break;
                case "Background color":
                    {
                        Console.BackgroundColor = color;
                        if (globalSettingsDictionary.ContainsKey(className))
                        {
                            globalSettingsDictionary[className].BackgroundColor = color.ToString();
                        }
                    }
                    break;
            }
        }

        #region ISettings Implementation

        /// <inheritdoc/>
        public Dictionary<string, ISettings> Read(string jsonPath)
        {
            try
            {
                if (File.Exists(jsonPath))
                {
                    var json = File.ReadAllText(jsonPath);
                    var settingsDictionary = JsonConvert.DeserializeObject<Dictionary<string, Settings>>(json);

                    // Check if settingsDictionary is null
                    if (settingsDictionary != null)
                    {
                        // Convert the values from Settings back to ISettings
                        var convertedDictionary = new Dictionary<string, ISettings>();
                        string[] classNames = GetClassNamesFromNamespace("SampleHierarchies.Gui");
                        int classesCount = ValidClassesCount(classNames);

                        foreach (var className in classNames)
                        {
                            if (IsNameValid(className))
                            {
                                // Use the null-conditional operator to handle potential null values
                                ISettings? settingsInstance = settingsDictionary.ContainsKey(className)
                                    ? settingsDictionary[className]
                                    : null; // Use null if the key is not found

                                if (settingsInstance != null)
                                {
                                    convertedDictionary[className] = settingsInstance;
                                }
                            }
                        }

                        return convertedDictionary;
                    }                   
                }
                else
                {
                    // Handle case where the file doesn't exist
                    Dictionary<string, ISettings> newDictionary = new Dictionary<string, ISettings>();
                    string[] classNames = GetClassNamesFromNamespace("SampleHierarchies.Gui");

                    foreach (var className in classNames)
                    {
                        if (IsNameValid(className))
                        {
                            // Create an instance of your settings class
                            ISettings settings = new Settings();

                            // Provide default values or handle null appropriately
                            settings.ForegroundColor = ConsoleColor.White.ToString();
                            settings.BackgroundColor = ConsoleColor.Black.ToString();

                            // Add it to the dictionary
                            newDictionary[className] = settings;
                        }
                    }

                    return newDictionary;
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log or throw)
                Console.WriteLine($"Error reading JSON: {ex.Message}");
                return globalSettingsDictionary;
            }

            // Return an empty dictionary if something goes wrong
            return new Dictionary<string, ISettings>();
        }

        /// <inheritdoc/>
        public void Save(Dictionary<string, ISettings> settingsDictionary, string jsonPath)
        {
            try
            {
                var json = JsonConvert.SerializeObject(settingsDictionary, Formatting.Indented);
                File.WriteAllText(jsonPath, json);
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log or throw)
                Console.WriteLine($"Error writing JSON: {ex.Message}");
            }
        }

        #endregion // ISettings Implementation

        public string[] GetClassNamesFromNamespace(string namespaceName)
        {
            // Get all assemblies currently loaded in the application domain
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // Filter assemblies by name
            assemblies = assemblies.Where(assembly => assembly.GetName().Name == "SampleHierarchies.Gui").ToArray();

            // Filter types in the specified namespace and select their names
            var classNames = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.Namespace == namespaceName)
                .Select(type => type.Name)
                .ToArray();

            return classNames;
        }

        public string GetCallingClassName()
        {
            // Create a stack trace and get the current method's stack frame.
            StackTrace stackTrace = new StackTrace();
            StackFrame? callingFrame = stackTrace.GetFrame(1); // 0 is the current frame, 1 is the calling frame.

            if (callingFrame != null)
            {
                // Get the method that called this method.
                MethodBase? callingMethod = callingFrame.GetMethod();

                if (callingMethod != null)
                {
                    // Get the class name of the calling method.
                    string? callingClassName = callingMethod.DeclaringType?.Name;

                    if (callingClassName != null)
                    {
                        return callingClassName;
                    }
                }
            }

            return "Class name not found";
        }

        bool IsNameValid(string name)
        {
            if (name.Contains("Screen") && !name.Equals("Screen"))
            {
                return true;
            }
            return false;
        }

        int ValidClassesCount(string[] classNames)
        {
            int count = 0;
            foreach (string className in classNames)
            {
                if (IsNameValid(className))
                {
                    count++;
                }
            }
            return count;
        }
    }
}
