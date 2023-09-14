using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleHierarchies.Data;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; // Add this using statement

namespace SampleHierarchies.Services.Tests
{
    [TestClass]
    public class ScreenDefinitionServiceTests
    {
        [TestMethod]
        public void Save_ValidScreenDefinition_SavesToJsonFile()
        {
            // Arrange           
            ScreenDefinition screenDefinition = new ScreenDefinition
            {
                lineEntries = new List<ScreenLineEntry>
                {
                    new ScreenLineEntry
                    (
                        ConsoleColor.Black.ToString(),
                        ConsoleColor.White.ToString(),
                        ""
                    )
                }
            };

            string jsonFileName = "valid.json";

            // Act
            ScreenDefinitionService.Save(screenDefinition, jsonFileName);

            // Assert
            var actualJson = File.ReadAllText(jsonFileName);
            ScreenDefinition? screenDefinition2 = JsonConvert.DeserializeObject<ScreenDefinition>(actualJson);

            if (screenDefinition2 == null)
            {
                throw new Exception("Deserialization failed. The JSON data couldn't be deserialized.");
            }

            Assert.AreEqual(screenDefinition2.lineEntries[0].backgroundColor, screenDefinition.lineEntries[0].backgroundColor); // Use JSON comparison
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))] // Adjust the expected exception type
        public void Save_InvalidJson_ThrowsException()
        {
            try
            {
                // Arrange
                ScreenDefinition screenDefinition = null; // Create a valid ScreenDefinition object
                string jsonFileName = "invalid.json"; // This could be an invalid file name


                // Act
                ScreenDefinitionService.Save(screenDefinition, jsonFileName);
            }
            catch
            {
                // Print the exception message for debugging
               // Console.WriteLine($"Exception Message: {ex.Message}");
                throw new Exception(); // Re-throw the exception to allow the test framework to catch it
            }
        }

        [TestMethod]
        public void Load_ValidJson_ReturnsScreenDefinition()
        {
            // Arrange
            string validJson = "{\"lineEntries\":[{\"backgroundColor\":\"Black\",\"foregroundColor\":\"White\",\"text\":\"\"}]}";
            string jsonFileName = "valid.json";

            // Create a temporary file with valid JSON content
            File.WriteAllText(jsonFileName, validJson);

            // Act
            var result = ScreenDefinitionService.Load(jsonFileName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ConsoleColor.Black.ToString(), result.lineEntries[0].backgroundColor);
            Assert.AreEqual(ConsoleColor.White.ToString(), result.lineEntries[0].foregroundColor);
            Assert.AreEqual("", result.lineEntries[0].text);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))] // Adjust the expected exception type
        public void Load_InvalidJson_ThrowsException()
        {
            // Arrange
            string invalidJson = "{ invalid json }";
            string jsonFileName = "invalid.json";

            // Create a temporary file with invalid JSON content
            File.WriteAllText(jsonFileName, invalidJson);

            // Act and Assert
            var result = ScreenDefinitionService.Load(jsonFileName);
        }
    }
}
