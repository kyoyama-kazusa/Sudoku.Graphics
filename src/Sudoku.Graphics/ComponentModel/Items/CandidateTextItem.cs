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
}
