namespace SampleHierarchies.Interfaces.Data.Mammals;

/// <summary>
/// Interface depicting a Elephant.
/// </summary>
public interface IAfricanElephant : IMammal
{
    #region Interface Members
    
    float height { get; set; }
    float weight { get; set; }
    float tuskLength { get; set; }
    int lifespan { get; set; }
    string socialBehavior { get; set; }

    #endregion // Interface Members
}
