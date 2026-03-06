namespace Sudoku.Graphics.Items;

/// <summary>
/// Represents canvas background fill item.
/// </summary>
public sealed record BackgroundFillItem : Item, IItem_ColorProperty
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.BackgroundFill;

	/// <inheritdoc/>
	public required SerializableColor Color { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas) => canvas.BackingCanvas.Clear(Color);
}
