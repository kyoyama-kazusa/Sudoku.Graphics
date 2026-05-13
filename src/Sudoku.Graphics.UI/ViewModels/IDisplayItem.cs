namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Indicates the base type of a display item.
/// </summary>
public interface IDisplayItem
{
	/// <summary>
	/// Indicates the value to display.
	/// </summary>
	object? ValueToDisplay { get; }
}
