using SampleHierarchies.Data;

namespace SampleHierarchies.Gui;

/// <summary>
/// Abstract base class for a screen.
/// </summary>
public abstract class Screen
{
    public ScreenDefinition? screenDefinition { get; set; }
    private string? screenDefinitionJson { get; set; }
    
    #region Public Methods

    /// <summary>
    /// Show the screen.
    /// </summary>
    public virtual void Show()
    {
        Console.WriteLine("Showing screen");
    }
    public void WriteCustomLine(int index)
    {
        if (screenDefinition != null)
        {
            var lineEntry = screenDefinition.lineEntries[index];
            Enum.TryParse(lineEntry.backgroundColor, out ConsoleColor backgroundColor);
            Enum.TryParse(lineEntry.foregroundColor, out ConsoleColor foregroundColor);
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(lineEntry.text);
        }
    }
    public void WriteHighLightedCustomLine(int index)
    {
        if (screenDefinition != null)
        {
            var lineEntry = screenDefinition.lineEntries[index];
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(lineEntry.text);
        }
    }

    public static List<string> history = new List<string>();
    public void HistoryShow () { 
        foreach (var entry in history)
        {
            Console.Write(" -> ");
            Console.Write(entry);

        }
            Console.WriteLine();
    }
    #endregion // Public Methods
}
