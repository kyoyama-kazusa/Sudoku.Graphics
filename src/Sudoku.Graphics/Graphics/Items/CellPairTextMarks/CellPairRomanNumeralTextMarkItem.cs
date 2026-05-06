namespace Sudoku.Graphics.Items.CellPairTextMarks;

/// <summary>
/// Represents a cell pair roman numeral text (1 - 1000) mark item.
/// </summary>
public sealed partial record CellPairRomanNumeralTextMarkItem : CellPairTextMarkItem
{
	/// <summary>
	/// Indicates the value.
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <see langword="value"/> is greater than 1000 or less than 1.
	/// </exception>
	public required int Value
	{
		get;

		init => field = value is > 1000 or <= 0 ? throw new ArgumentOutOfRangeException(nameof(value)) : value;
	}

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPairText_RomanNumeral;

	/// <inheritdoc/>
	protected override string PrintingText => Notation.ToNumeralString(Value);
}
