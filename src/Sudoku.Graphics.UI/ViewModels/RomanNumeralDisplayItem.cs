namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents roman numeral display item.
/// </summary>
public sealed class RomanNumeralDisplayItem : ITextDisplayItem
{
	/// <summary>
	/// Indicates the value.
	/// </summary>
	public int Value { get; set; }

	/// <summary>
	/// Indicates the value string.
	/// </summary>
	public string ValueString => CellPairRomanNumeralTextMarkItem.Notation.ToNumeralString(Value);

	/// <inheritdoc/>
	string ITextDisplayItem.Text => ValueString;
}
