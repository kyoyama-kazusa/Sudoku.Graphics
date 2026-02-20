namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents candidate text.
/// </summary>
public sealed class CandidateTextItem : SmallTextItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CandidateText;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CandidateTextItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		GetColorFieldUnsafe(this) = canvas.Options.TextColorSet.Resolve(canvas.Options)[2];
		base.DrawTo(canvas);
	}
}
