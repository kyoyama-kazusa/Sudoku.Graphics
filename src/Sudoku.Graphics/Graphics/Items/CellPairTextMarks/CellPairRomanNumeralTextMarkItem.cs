namespace Sudoku.Graphics.Items.CellPairTextMarks;

/// <summary>
/// Represents a cell pair roman numeral text (1 - 1000) mark item.
/// </summary>
public sealed record CellPairRomanNumeralTextMarkItem : CellPairTextMarkItem
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
	protected override string PrintingText
	{
		get
		{
			var values = (int[])[1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1];
			var symbols = (string[])["M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"];
			var sb = new StringBuilder();
			for (var (i, number) = (0, Value); i < values.Length; i++)
			{
				while (number >= values[i])
				{
					number -= values[i];
					sb.Append(symbols[i]);
				}
			}
			return sb.ToString();
		}
	}
}
