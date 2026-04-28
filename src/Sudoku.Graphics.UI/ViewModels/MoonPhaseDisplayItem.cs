namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents a moon phase display item.
/// </summary>
public sealed class MoonPhaseDisplayItem
{
	/// <summary>
	/// Indicates moon phase.
	/// </summary>
	public MoonPhase Phase { get; set; }

	/// <summary>
	/// Indicates image source.
	/// </summary>
	public ImageSource? Source { get; set; }
}
