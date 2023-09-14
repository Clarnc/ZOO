using SampleHierarchies.Interfaces.Data;

namespace SampleHierarchies.Interfaces.Services;

public interface ISettingsService
{
    #region Interface Members
    Dictionary<string, ISettings> globalSettingsDictionary { get; set; }
    /// <summary>
    /// Read settings.
    /// </summary>
    /// <param name="jsonPath">Json path</param>
    /// <returns></returns>
    Dictionary<string, ISettings> Read(string jsonPath);

    /// <summary>
    /// Write settings.
    /// </summary>
    /// <param name="settings">Settings to written</param>
    /// <param name="jsonPath">Json path</param>
    void Save(Dictionary<string, ISettings> settingsDictionary, string jsonPath);

    void ApplySettings(string className);

    void ApplySettings(string className,string property, ConsoleColor color);

    string GetCallingClassName();
    #endregion // Interface Members
}
