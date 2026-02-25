namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents given text.
/// </summary>
public sealed class GivenTextItem : GivenOrModifiableTextItem
{
	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(GivenTextItem);
}
