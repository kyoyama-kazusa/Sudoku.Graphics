namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates cell Roman numeral font name.
	/// </summary>
	public Inherited<string> CellRomanNumeralFontName { get; set; } = Inherited<string>.FromPropertyName(nameof(DefaultCellPairTextFontWidth));

	/// <summary>
	/// Indicates cell Roman numeral font slant.
	/// </summary>
	public Inherited<SKFontStyleSlant> CellRomanNumeralFontSlant { get; set; } = Inherited<SKFontStyleSlant>.FromPropertyName(nameof(DefaultCellPairTextFontWeight));

	/// <summary>
	/// Indicates cell Roman numeral font width.
	/// </summary>
	public Inherited<SKFontStyleWidth> CellRomanNumeralFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromPropertyName(nameof(DefaultCellPairTextFontWidth));

	/// <summary>
	/// Indicates cell Roman numeral font weight.
	/// </summary>
	public Inherited<SKFontStyleWeight> CellRomanNumeralFontWeight { get; set; } = Inherited<SKFontStyleWeight>.FromPropertyName(nameof(DefaultCellPairTextFontWeight));

	/// <summary>
	/// Indicates cell Roman numeral font size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellRomanNumeralFontSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultCellPairTextFontSizeScale));

	/// <summary>
	/// Indicates cell Roman numeral font color.
	/// </summary>
	public Inherited<SerializableColor> CellRomanNumeralFontColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultCellPairTextFontColor));

	/// <summary>
	/// Indicates cell Roman numeral cover color.
	/// </summary>
	public Inherited<SerializableColor> CellRomanNumeralCoverColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultCellPairTextCoverColor));

	/// <summary>
	/// Indicates cell Roman numeral padding.
	/// </summary>
	public Inherited<Thickness<float>> CellRomanNumeralPadding { get; set; } = Inherited<Thickness<float>>.FromPropertyName(nameof(DefaultCellPairTextPadding));
}
