namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell border-aligned digit font name.
	/// </summary>
	public Inherited<string> CellBorderAlignedDigitFontName { get; set; } = Inherited<string>.FromPropertyName(nameof(DefaultFontName));

	/// <summary>
	/// Indicates the cell border-aligned digit font size scale (related to cell size).
	/// </summary>
	public Inherited<Scale> CellBorderAlignedDigitFontSizeScale { get; set; } = Inherited<Scale>.FromValue(0.4M);

	/// <summary>
	/// Indicates cell border-aligned digit text color.
	/// </summary>
	public Inherited<SerializableColor> CellBorderAlignedDigitFontColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultFontColor));

	/// <summary>
	/// Indicates cell border-aligned digit font slant.
	/// </summary>
	public Inherited<SKFontStyleSlant> CellBorderAlignedDigitFontSlant { get; set; } = Inherited<SKFontStyleSlant>.FromPropertyName(nameof(DefaultFontSlant));

	/// <summary>
	/// Indicates cell border-aligned digit font width.
	/// </summary>
	public Inherited<SKFontStyleWidth> CellBorderAlignedDigitFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromPropertyName(nameof(DefaultFontWidth));

	/// <summary>
	/// Indicates cell border-aligned digit font weight.
	/// </summary>
	public Inherited<SKFontStyleWeight> CellBorderAlignedDigitFontWeight { get; set; } = Inherited<SKFontStyleWeight>.FromPropertyName(nameof(DefaultFontWeight));
}
