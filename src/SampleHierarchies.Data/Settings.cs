using SampleHierarchies.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Data
{
    public class Settings : ISettings
    {
        public Settings()
        {
            // Initialize properties with default values or appropriate non-null values
            Version = "1.0"; // Example default value
            BackgroundColor = "White"; // Example default value
            ForegroundColor = "Black"; // Example default value
        }

        public string? Version { get; set; } // Add ? to match the interface
        public string? BackgroundColor { get; set; }
        public string? ForegroundColor { get; set; }
    }
}
