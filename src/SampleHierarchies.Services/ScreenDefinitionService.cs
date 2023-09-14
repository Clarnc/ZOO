using SampleHierarchies.Data;
using Newtonsoft.Json;
using SampleHierarchies.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Services
{
    public static class ScreenDefinitionService
    {
        public static ScreenDefinition Load(string jsonFileName) {
            try
            {
                var json = File.ReadAllText(jsonFileName);
                ScreenDefinition? screenDefinition = JsonConvert.DeserializeObject<ScreenDefinition>(json);

                if (screenDefinition != null)
                {
                    return screenDefinition;
                }
                else
                {
                    // Handle the case where deserialization fails, such as logging an error or returning a default value.
                    return new ScreenDefinition(); 
                }
            }
            catch 
            {
                // Handle exception (e.g., log or throw)
                //Console.WriteLine($"Error loading JSON: {ex.Message}");
                throw new Exception();
            }
        }
        public static void Save(ScreenDefinition screenDefinition, string jsonFileName) {
            try
            {if (screenDefinition != null)
                {
                    var json = JsonConvert.SerializeObject(screenDefinition, Formatting.Indented);
                    File.WriteAllText(jsonFileName, json);
                }else throw new Exception();
            }
            catch 
            {
                // Handle exception (e.g., log or throw)
                //Console.WriteLine($"Error saving JSON: {ex.Message}");
                throw new Exception();
            }
        }
    }
}
