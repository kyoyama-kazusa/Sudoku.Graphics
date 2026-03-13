namespace Sudoku.Graphics.Items.Fills;

/// <summary>
/// Represents a fill item.
/// </summary>
public abstract record FillItem : Item, IItem_ColorProperty
{
	/// <inheritdoc/>
	public required SerializableColor Color { get; init; }
}
