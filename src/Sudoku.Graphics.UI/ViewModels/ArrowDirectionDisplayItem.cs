namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents arrow direction display item.
/// </summary>
public sealed class ArrowDirectionDisplayItem
{
	/// <summary>
	/// Indicates the direction string.
	/// </summary>
	public string DirectionString => Direction.ArrowString;

	/// <summary>
	/// Indicates the direction.
	/// </summary>
	public Direction8 Direction { get; set; }
}
