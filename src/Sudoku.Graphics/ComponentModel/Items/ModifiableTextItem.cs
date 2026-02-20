namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents modifiable text.
/// </summary>
public sealed class ModifiableTextItem : BigTextItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.ModifiableText;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(ModifiableTextItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		GetColorFieldUnsafe(this) = canvas.Options.TextColorSet.Resolve(canvas.Options)[1];
		base.DrawTo(canvas);
	}
}
