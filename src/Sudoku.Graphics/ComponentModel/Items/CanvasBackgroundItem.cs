namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents canvas background item.
/// </summary>
public sealed class CanvasBackgroundItem : BackgroundItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CanvasBackground;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CanvasBackgroundItem);


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] Item? other) => other is CanvasBackgroundItem;

	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(EqualityContract);

	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.Clear(canvas.Options.BackgroundColor.Resolve(canvas.Options));

	/// <inheritdoc/>
	protected override void PrintMembers(StringBuilder builder)
	{
	}
}
