namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents a moon phase display item.
/// </summary>
public sealed class MoonPhaseDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates moon phase.
	/// </summary>
	public MoonPhase Phase { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}
