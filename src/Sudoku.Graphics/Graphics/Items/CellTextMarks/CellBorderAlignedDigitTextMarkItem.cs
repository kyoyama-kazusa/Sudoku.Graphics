namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents a cell border-aligned digit text mark item.
/// </summary>
public sealed record CellBorderAlignedDigitTextMarkItem : CellTextMarkItem
{
	/// <summary>
	/// Indicates digit, base 0.
	/// </summary>
	public required int Digit { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellText_BorderAlignedDigit;

	/// <inheritdoc/>
	protected override string PrintingText => (Digit + 1).ToString();
}
