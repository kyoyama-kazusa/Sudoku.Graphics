namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates cell number font name.
	/// </summary>
	public Inherited<string> CellNumberFontName { get; set; } = Inherited<string>.FromPropertyName(nameof(DefaultCellPairTextFontName));

	/// <summary>
	/// Indicates cell number font slant.
	/// </summary>
	public Inherited<SKFontStyleSlant> CellNumberFontSlant { get; set; } = Inherited<SKFontStyleSlant>.FromPropertyName(nameof(DefaultCellPairTextFontSlant));

	/// <summary>
	/// Indicates cell number font width.
	/// </summary>
	public Inherited<SKFontStyleWidth> CellNumberFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromPropertyName(nameof(DefaultCellPairTextFontWidth));

	/// <summary>
	/// Indicates cell number font weight.
	/// </summary>
	public Inherited<SKFontStyleWeight> CellNumberFontWeight { get; set; } = Inherited<SKFontStyleWeight>.FromPropertyName(nameof(DefaultCellPairTextFontWeight));

	/// <summary>
	/// Indicates cell number font size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellNumberFontSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultCellPairTextFontSizeScale));

	/// <summary>
	/// Indicates cell number font color.
	/// </summary>
	public Inherited<SerializableColor> CellNumberFontColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultCellPairTextFontColor));

	/// <summary>
	/// Indicates cell number cover color.
	/// </summary>
	public Inherited<SerializableColor> CellNumberCoverColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultCellPairTextCoverColor));

	/// <summary>
	/// Indicates cell number padding.
	/// </summary>
	public Inherited<Thickness<float>> CellNumberPadding { get; set; } = Inherited<Thickness<float>>.FromPropertyName(nameof(DefaultCellPairTextPadding));
}
