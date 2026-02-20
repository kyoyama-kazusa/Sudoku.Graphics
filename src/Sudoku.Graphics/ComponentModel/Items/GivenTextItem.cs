namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents given text.
/// </summary>
public sealed class GivenTextItem : BigTextItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.GivenText;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(GivenTextItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		GetColorFieldUnsafe(this) = canvas.Options.TextColorSet.Resolve(canvas.Options)[0];
		base.DrawTo(canvas);
	}
}
