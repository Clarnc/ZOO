namespace SampleHierarchies.Interfaces.Data;

/// <summary>
/// Settings interface.
/// </summary>
public interface ISettings
{
    #region Interface Members

    /// <summary>
    /// Version of settings.
    /// Console background color   
    /// Console foreground color
    /// </summary>
    public string? Version { get; set; }
    public string? BackgroundColor { get; set; }
    public string? ForegroundColor { get; set; }

    #endregion // Interface Members
}

