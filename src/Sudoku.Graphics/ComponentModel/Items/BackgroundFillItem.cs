namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents canvas background fill item.
/// </summary>
public sealed class BackgroundFillItem : FillItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.BackgroundFill;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(BackgroundFillItem);


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] Item? other) => other is BackgroundFillItem;

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
