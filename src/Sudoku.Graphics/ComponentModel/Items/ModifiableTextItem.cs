namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents modifiable text.
/// </summary>
public sealed class ModifiableTextItem : GivenOrModifiableTextItem
{
	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(ModifiableTextItem);
}
