namespace Sudoku.Graphics.Items.Fills;

/// <summary>
/// Represents canvas background fill item.
/// </summary>
public sealed record BackgroundFillItem : FillItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.BackgroundFill;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas) => canvas.BackingCanvas.Clear(Color);
}
