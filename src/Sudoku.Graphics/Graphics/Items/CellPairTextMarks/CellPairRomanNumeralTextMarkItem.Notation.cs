namespace Sudoku.Graphics.Items.CellPairTextMarks;

public partial record CellPairRomanNumeralTextMarkItem
{
	/// <summary>
	/// Provides a way to print Roman numerals.
	/// </summary>
	public static class Notation
	{
		/// <summary>
		/// Returns a <see cref="string"/> value that displays the same value of the specified number, in Roman numeral format.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The string value.</returns>
		public static string ToNumeralString(int value)
		{
			var values = (int[])[1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1];
			var symbols = (string[])["M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"];
			var sb = new StringBuilder();
			for (var (i, number) = (0, value); i < values.Length; i++)
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
