namespace SampleHierarchies.Interfaces.Data.Mammals;

/// <summary>
/// Interface depicting a monkey.
/// </summary>
public interface IChimpanzee : IMammal
{
    #region Interface Members
    
    bool opposableThumbs { get; set; }
    string socialBehavior { get; set; }
    bool toolUse { get; set; }
    int intelligence { get; set; }
    string diet {get; set; }
    #endregion // Interface Members
}
