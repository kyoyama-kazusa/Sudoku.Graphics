namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents a cell suit mark item.
/// </summary>
public sealed class CellSuitTextMarkItem : CellTextMarkItem
{
	/// <summary>
	/// Indicates the suit.
	/// </summary>
	public required Suit Suit { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_SuitText;

	/// <inheritdoc/>
	protected override string PrintingText
		=> Suit switch
		{
			Suit.Spade => "\u2660",
			Suit.Heart => "\u2665",
			Suit.Club => "\u2663",
			Suit.Diamond => "\u2666",
			_ => string.Empty
		};

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellSuitTextMarkItem);
}
