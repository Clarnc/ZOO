namespace SampleHierarchies.Interfaces.Data.Mammals;

/// <summary>
/// Interface depicting a bear.
/// </summary>
public interface IPolarBear : IMammal
{
    #region Interface Members
    
    string furCoat { get; set; }
    string paws { get; set; }
    string carnivorousDiet { get; set; }
    bool semiAquatic { get; set; }
    string senceOfSmell { get; set; }

    #endregion // Interface Members
}
