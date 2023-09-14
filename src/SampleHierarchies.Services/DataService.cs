using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;
using SampleHierarchies.Interfaces.Services;
using System;
using System.Diagnostics;
using System.IO;

namespace SampleHierarchies.Services
{
    /// <summary>
    /// Implementation of data service.
    /// </summary>
    public class DataService : IDataService
    {
        #region IDataService Implementation

        /// <inheritdoc/>
        public IAnimals? Animals { get; set; }

        /// <inheritdoc/>
        public bool Read(string jsonPath)
        {
            bool result = true;

            try
            {
                if (File.Exists(jsonPath))
                {
                    string jsonContent = File.ReadAllText(jsonPath);

                    // Load JSON as a JObject to access the "Mammals" property
                    JObject jsonObject = JObject.Parse(jsonContent);

                    // Initialize mammals with default values (empty lists)
                    var mammals = new Mammals
                    {
                        Dogs = new List<IDog>(),
                        AfricanElephants = new List<IAfricanElephant>(),
                        PolarBears = new List<IPolarBear>(),
                        Chimpanzees = new List<IChimpanzee>(),
                    };

                    var dogsToken = jsonObject["Mammals"]?["Dogs"];
                    if (dogsToken != null)
                    {
                        var dogs = dogsToken.ToObject<List<Dog>>();
                        if (dogs != null)
                        {
                            mammals.Dogs = dogs.Cast<IDog>().ToList();
                        }
                    }

                    var elephantsToken = jsonObject["Mammals"]?["AfricanElephants"];
                    if (elephantsToken != null)
                    {
                        var elephants = elephantsToken.ToObject<List<AfricanElephant>>();
                        if (elephants != null)
                        {
                            mammals.AfricanElephants = elephants.Cast<IAfricanElephant>().ToList();
                        }
                    }

                    var bearsToken = jsonObject["Mammals"]?["PolarBears"];
                    if (bearsToken != null)
                    {
                        var bears = bearsToken.ToObject<List<PolarBear>>();
                        if (bears != null)
                        {
                            mammals.PolarBears = bears.Cast<IPolarBear>().ToList();
                        }
                    }

                    var chimpsToken = jsonObject["Mammals"]?["Chimpanzees"];
                    if (chimpsToken != null)
                    {
                        var chimps = chimpsToken.ToObject<List<Chimpanzee>>();
                        if (chimps != null)
                        {
                            mammals.Chimpanzees = chimps.Cast<IChimpanzee>().ToList();
                        }
                    }

                    // Create the Animals instance and set the mammals
                    Animals = new Animals { Mammals = mammals };
                }
                else
                {
                    Console.WriteLine("The JSON file does not exist at the specified path.");
                    result = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                result = false;
            }

            return result;
        }

        /// <inheritdoc/>
        public bool Write(string jsonPath)
        {
            bool result = true;

            try
            {
                var jsonSettings = new JsonSerializerSettings();
                string jsonContent = JsonConvert.SerializeObject(Animals);
                string jsonContentFormatted = jsonContent.FormatJson();
                File.WriteAllText(jsonPath, jsonContentFormatted);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                result = false;
            }

            return result;
        }

        #endregion // IDataService Implementation

        #region Ctors

        /// <summary>
        /// Default ctor.
        /// </summary>
        public DataService()
        {
            Animals = new Animals();
        }

        #endregion // Ctors
    }
}
