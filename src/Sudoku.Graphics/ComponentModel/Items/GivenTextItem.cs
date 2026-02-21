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
}
