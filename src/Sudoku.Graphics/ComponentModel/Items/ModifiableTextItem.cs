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
}
