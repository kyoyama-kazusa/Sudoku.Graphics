namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents a cell suit mark item.
/// </summary>
public sealed record CellSuitTextMarkItem : CellTextMarkItem
{
	/// <summary>
	/// Indicates the suit.
	/// </summary>
	public required Suit Suit { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellText_Suit;

	/// <inheritdoc/>
	protected override string PrintingText => Enum.IsDefined(Suit) ? Suit.EquivalentChar.ToString() : string.Empty;
}
